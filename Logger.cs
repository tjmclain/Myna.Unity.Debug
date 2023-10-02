using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Myna.Unity.Debug
{
	// In order to navigate to where the original call was made from when I double click the log in the console,
	// I need to export my debug scripts as a separate DLL
	public class Logger
	{
		public static readonly Logger Default = new Logger();
		private static readonly ILogger _unityLogger;
		private static readonly string _namespace = typeof(Logger).Namespace;
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

		public LogType FilterLogType { get; set; } = LogType.Log;

		public LogType EditModeFilterLogType { get; set; } = LogType.Log;

		public string? CallerTypeName { get; set; } = null;

		#region Constructors

		static Logger()
		{
			_unityLogger = UnityEngine.Debug.unityLogger;
		}

		#endregion Constructors

		#region Static Interface

		public static string FormatTag(string callerTypeName, string methodName)
		{
			var sb = new StringBuilder();
			if (!string.IsNullOrEmpty(callerTypeName))
			{
				sb.Append(callerTypeName);
			}

			if (!string.IsNullOrEmpty(methodName))
			{
				if (sb.Length > 0)
				{
					sb.Append(".");
				}
				sb.Append(methodName);
			}

			return sb.ToString();
		}

		// https://stackoverflow.com/questions/16466380/get-user-friendly-name-for-generic-type-in-c-sharp
		public static string GetFriendlyTypeName(Type type)
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

		#endregion Static Interface

		#region Getter/Setter Methods

		public bool IsLogTypeAllowed(LogType logType)
		{
			var allowedLogType = Application.isPlaying
				? FilterLogType
				: EditModeFilterLogType;

			return (int)logType <= (int)allowedLogType;
		}

		#endregion Getter/Setter Methods

		#region Log Methods

		public void Log(object message)
			=> Log(LogType.Log, message);

		public void LogError(object message)
			=> Log(LogType.Error, message);

		public void LogException(Exception exception)
			=> UnityEngine.Debug.LogException(exception);

		public void LogFormat(LogType logType, string format, params object[] args)
			=> Log(logType, string.Format(format, args));

		public void LogWarning(object message)
			=> Log(LogType.Warning, message);

		public void Log(LogType logType, object message)
			=> Log(logType, string.Empty, message);

		public void Log(LogType logType, object message, UnityEngine.Object context)
		{
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			using var log = CreateMessage(logType);

			log.Context = context;
			log.Append(message.ToString());
			log.Print();
		}

		public void Log(LogType logType, string tag, object message)
		{
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			using var log = CreateMessage(logType);

			log.Tag = tag;
			log.Append(message.ToString());
			log.Print();
		}

		public void Log(Message message)
		{
			if (!IsLogTypeAllowed(message.LogType))
			{
				return;
			}

			string tag = string.IsNullOrEmpty(message.Tag)
				? GetDefaultTag() : message.Tag;

			if (!string.IsNullOrEmpty(tag))
			{
				_unityLogger.Log(message.LogType, tag, message, message.Context);
			}
			else
			{
				_unityLogger.Log(message.LogType, message, message.Context);
			}
		}

		#endregion Log Methods

		#region DebugLogBuilder Utility Methods

		public Message CreateMessage(LogType logType)
		{
			var log = Message.Create(logType);
			log.Logger = this;
			return log;
		}

		#endregion DebugLogBuilder Utility Methods

		// https://answers.unity.com/questions/289006/catching-double-clicking-console-messages.html
		private static string GetDefaultTag()
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
				callerTypeName ??= coroutineMatch.Groups[1].Value;
				methodName ??= coroutineMatch.Groups[2].Value;
				return true;
			}

			// Are we an anonymous method?
			var anonMethodMatch = _anonMethodRegex.Match(method.Name);
			if (anonMethodMatch.Success)
			{
				callerTypeName ??= GetFriendlyTypeName(callerTypeInfo);
				methodName = anonMethodMatch.Groups[1].Value;
				return true;
			}

			// Are we a local method?
			var localMethodMatch = _localMethodRegex.Match(method.Name);
			if (localMethodMatch.Success)
			{
				callerTypeName ??= GetFriendlyTypeName(callerTypeInfo);
				methodName = localMethodMatch.Groups[1].Value;
				return true;
			}

			// If we're not a special case, set to the default caller type and method tags
			callerTypeName ??= GetFriendlyTypeName(callerTypeInfo);
			methodName ??= method.Name;
			return true;
		}
	}
}