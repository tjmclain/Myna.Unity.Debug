using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Myna.Unity.Debug
{
	// In order to navigate to where the original call was made from when I double click the log in the console, 
	// I need to export my debug scripts as a separate DLL
	public class Logger
	{
		private LogType _filterLogType = LogType.Exception;

		private static readonly Regex _anonMethodRegex = new Regex(@"^<(\w+)>b_.+$");

		[Conditional("DEBUG")]
		public void Log(string message) =>
			Log(LogType.Log, message);

		[Conditional("DEBUG")]
		public void LogWarning(string message) =>
			Log(LogType.Warning, message);

		[Conditional("DEBUG")]
		public void LogError(string message) =>
			Log(LogType.Error, message);

		public void LogException(Exception exception) =>
			UnityEngine.Debug.LogException(exception);

		public void FilterLogType(LogType logType)
		{
			_filterLogType = logType;
		}

		public bool IsFiltered(LogType logType)
		{
			return logType >= _filterLogType && logType != LogType.Exception;
		}

		internal void Log(LogType logType, string message)
		{
			if (IsFiltered(logType))
			{
				return;
			}

			if (TryGetTags(out string typeName, out string methodName))
			{
				var sb = new StringBuilder();
				if (!string.IsNullOrEmpty(typeName))
				{
					sb.Append('[');
					sb.Append(typeName);
					sb.Append("] ");
				}

				if (!string.IsNullOrEmpty(typeName))
				{
					sb.Append(methodName);
					sb.Append(": ");
				}

				sb.Append(message);
				message = sb.ToString();
			}

			UnityEngine.Debug.unityLogger.Log(logType, message);
		}

		// https://answers.unity.com/questions/289006/catching-double-clicking-console-messages.html
		internal static bool TryGetTags(out string className, out string methodName)
		{
			className = null;
			methodName = null;

			var stackTrace = new StackTrace();

			// Not sure why, but index 3 is (usually) the correct level
			int index = Mathf.Min(stackTrace.FrameCount - 1, 3);
			if (index < 0)
			{
				return false;
			}

			var frame = stackTrace.GetFrame(index);
			var method = frame.GetMethod();

			if (TryGetAnonymousMethodTags(method, stackTrace, index, out className, out methodName))
			{
				return true;
			}

			var typeInfo = method.ReflectedType ?? method.DeclaringType;
			className = GetFriendlyTypeName(typeInfo);
			methodName = method.Name;
			return true;
		}

		// Check if our method represents an anonymous method:
		// https://stackoverflow.com/questions/23228075/determine-if-methodinfo-represents-a-lambda-expression
		// NOTE: I tried the above, and it didn't do what I wanted it to do
		// (It skipped the anonymous method, but the method it *did* display wasn't right)
		// So, try getting a friendlier name with regex instead
		internal static bool TryGetAnonymousMethodTags(
			MethodBase method,
			StackTrace stackTrace,
			int frameIndex,
			out string className,
			out string methodName
		)
		{
			className = null;
			methodName = null;

			var match = _anonMethodRegex.Match(method.Name);
			if (!match.Success)
			{
				return false;
			}

			methodName = match.Groups[1].Value;
			// This is the best solution to find the 'typeName' I could think of unfortunately
			// If I call 'DeclaringType' or 'ReflectedType' on the methodbase
			// I get '<>c', where a return type would go in the braces I think
			for (int i = frameIndex + 1; i < stackTrace.FrameCount; i++)
			{
				var frame = stackTrace.GetFrame(i);
				method = frame.GetMethod();
				if (method.Name == methodName)
				{
					var typeInfo = method.ReflectedType ?? method.DeclaringType;
					className = GetFriendlyTypeName(typeInfo);
					break;
				}
			}

			return true;
		}

		// https://stackoverflow.com/questions/16466380/get-user-friendly-name-for-generic-type-in-c-sharp
		internal static string GetFriendlyTypeName(Type type)
		{
			if (type == null)
			{
				return null;
			}

			if (type == typeof(int))
				return "int";
			else if (type == typeof(short))
				return "short";
			else if (type == typeof(byte))
				return "byte";
			else if (type == typeof(bool))
				return "bool";
			else if (type == typeof(long))
				return "long";
			else if (type == typeof(float))
				return "float";
			else if (type == typeof(double))
				return "double";
			else if (type == typeof(decimal))
				return "decimal";
			else if (type == typeof(string))
				return "string";
			else if (type.IsGenericType)
			{
				var sb = new StringBuilder();

				string name = type.Name;
				int length = name.IndexOf('`');
				string mainName = name.Substring(0, length);
				sb.Append(mainName);

				sb.Append('<');
				string argNames = type.GetGenericArguments()
					.Select(x => GetFriendlyTypeName(x))
					.Aggregate((x, y) => $"{x}, {y}");
				sb.Append(argNames);
				sb.Append('>');

				name = sb.ToString();
			}

			return type.Name;
		}


	}
}