using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Myna.Unity.Debug
{
	public static class TagUtility
	{
		private static readonly string _namespace = nameof(Unity.Debug);

		private static readonly Regex _coroutineRegex = new Regex(@"^(\S+)\+<(\S+)>d__\d+$");
		private static readonly Regex _anonMethodRegex = new Regex(@"^<(\S+)>b__\d+_\d+$");
		private static readonly Regex _localMethodRegex = new Regex(@"^<(\S+)>g__\S+|\d+_\d+$");

		private static readonly Dictionary<Type, string> _friendlyTypeNames = new Dictionary<Type, string>()
		{
			{ typeof(int), "int" },
			{ typeof(short), "short" },
			{ typeof(long), "long" },
			{ typeof(byte), "byte" },
			{ typeof(float), "float" },
			{ typeof(double), "double" },
			{ typeof(decimal), "decimal" },
			{ typeof(string), "string" },
			{ typeof(bool), "bool" },
		};

		public static string FormatTag(string caller, string methodName)
		{
			return $"{caller}.{methodName}";
		}

		public static string FormatTag(object caller, string methodName)
		{
			return FormatTag(caller.GetType().Name, methodName);
		}

		public static string GetDefaultTag()
		{
			return TryGetCallerTypeAndMethodName(out string callerTypeName, out string methodName)
				? FormatTag(callerTypeName, methodName)
				: string.Empty;
		}

		private static bool TryGetCallerTypeAndMethodName(out string callerTypeName, out string methodName)
		{
			static bool GetStackFrameIndex(StackTrace stackTrace, out int index)
			{
				for (int i = 1; i < stackTrace.FrameCount; i++)
				{
					var frame = stackTrace.GetFrame(i);
					var method = frame.GetMethod();
					var type = method.DeclaringType;
					if (type.Namespace != _namespace)
					{
						index = i;
						return true;
					}
				}

				index = -1;
				return false;
			}

			callerTypeName = default;
			methodName = default;

			var stackTrace = new StackTrace();
			if (stackTrace.FrameCount == 0)
			{
				return false;
			}

			if (!GetStackFrameIndex(stackTrace, out int index))
			{
				return false;
			}

			var frame = stackTrace.GetFrame(index);
			var method = frame.GetMethod();
			var callerTypeInfo = method.ReflectedType ?? method.DeclaringType;

			// Are we a coroutine?
			var coroutineMatch = _coroutineRegex.Match(callerTypeInfo.ToString());
			if (coroutineMatch.Success)
			{
				callerTypeName = coroutineMatch.Groups[1].Value;
				methodName = coroutineMatch.Groups[2].Value;
				return true;
			}

			// Are we an anonymous method?
			var anonMethodMatch = _anonMethodRegex.Match(method.Name);
			if (anonMethodMatch.Success)
			{
				callerTypeName = GetFriendlyTypeName(callerTypeInfo);
				methodName = anonMethodMatch.Groups[1].Value;
				return true;
			}

			// Are we a local method?
			var localMethodMatch = _localMethodRegex.Match(method.Name);
			if (localMethodMatch.Success)
			{
				callerTypeName = GetFriendlyTypeName(callerTypeInfo);
				methodName = localMethodMatch.Groups[1].Value;
				return true;
			}

			// If we're not a special case, set to the default caller type and method tags
			callerTypeName = GetFriendlyTypeName(callerTypeInfo);
			methodName = method.Name;
			return true;
		}

		private static string GetFriendlyTypeName(Type type)
		{
			if (type == null)
			{
				return string.Empty;
			}

			if (_friendlyTypeNames.TryGetValue(type, out string result))
			{
				return result;
			}

			if (type.IsGenericType)
			{
				var sb = new StringBuilder();

				string name = type.Name;
				int length = name.IndexOf('`');
				string mainName = name[..length];
				sb.Append(mainName);

				sb.Append('<');
				string argNames = type.GetGenericArguments()
					.Select(x => GetFriendlyTypeName(x))
					.Aggregate((x, y) => $"{x}, {y}");
				sb.Append(argNames);
				sb.Append('>');
				return sb.ToString();
			}

			return type.Name;
		}
	}
}