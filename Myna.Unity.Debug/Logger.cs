using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Myna.Unity.Debug
{
	// In order to navigate to where the original call was made from when I double click the log in the console,
	// I need to export my debug scripts as a separate DLL
	public class Logger
	{
		private LogType _filterLogType = LogType.Log;

		public LogType FilterLogType
		{
			get => _filterLogType;
			set => _filterLogType = value;
		}

		private LogType _editModeFilterLogType = LogType.Log;

		public LogType EditModeFilterLogType
		{
			get => _editModeFilterLogType;
			set => _editModeFilterLogType = value;
		}

		private string? _callerTypeName = null;

		public string? CallerTypeName
		{
			get => _callerTypeName;
			set => _callerTypeName = value;
		}

		public static readonly Logger Default = new Logger();

		private static readonly ILogger _unityLogger;
		private static readonly string _namespace;

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

		#region Constructors

		public Logger()
		{
			_filterLogType = LogType.Log;
			_editModeFilterLogType = LogType.Log;
		}

		static Logger()
		{
			_unityLogger = UnityEngine.Debug.unityLogger;
			_namespace = typeof(Logger).Namespace;
		}

		#endregion Constructors

		#region Static Interface

		public static string GetTag(object caller, string methodName)
			=> GetTag(caller.GetType(), methodName);

		public static string GetTag(Type callerType, string methodName)
			=> FormatTag(callerType.Name, methodName);

		public static string FormatTag(string callerTypeName, string methodName)
		{
			var sb = new StringBuilder();
			if (!string.IsNullOrEmpty(callerTypeName))
			{
				sb.Append('[');
				sb.Append(callerTypeName);
				sb.Append("]");
			}

			if (!string.IsNullOrEmpty(methodName))
			{
				if (sb.Length > 0)
				{
					sb.Append(" ");
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
#if UNITY_EDITOR
			var allowedLogType = EditorApplication.isPlaying
				? _filterLogType
				: _editModeFilterLogType;
			return (int)logType <= (int)allowedLogType;
#else
			return (int)logType <= (int)_filterLogType;
#endif
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
			using (var log = CreateLog(logType))
			{
				log.Context = context;
				log.Append(message.ToString());
				Log(log);
			}
		}

		public void Log(LogType logType, string tag, object message)
		{
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			if (!string.IsNullOrEmpty(tag) || TryGetTag(ref tag))
			{
				_unityLogger.Log(logType, tag, message);
			}
			else
			{
				_unityLogger.Log(logType, message);
			}
		}

		public void Log(DebugLogBuilder log)
		{
			if (!IsLogTypeAllowed(log.LogType))
			{
				return;
			}

			string? callerTypeName = log.CallerTypeName;
			string? methodName = log.MethodName;

			if (TryGetCallerTypeAndMethodName(ref callerTypeName, ref methodName))
			{
				string tag = FormatTag(callerTypeName, methodName);
				_unityLogger.Log(log.LogType, tag, log.Message, log.Context);
			}
			else
			{
				_unityLogger.Log(log.LogType, (object)log.Message, log.Context);
			}
		}

		#endregion Log Methods

		#region DebugLogBuilder Utility Methods

		public DebugLogBuilder CreateLog(LogType logType)
		{
			var log = DebugLogBuilder.Create(logType);
			log.CallerTypeName = CallerTypeName;
			log.Logger = this;
			return log;
		}

		public DebugLogBuilder CreateLog()
			=> CreateLog(LogType.Log);

		public DebugLogBuilder CreateWarning()
			=> CreateLog(LogType.Warning);

		public DebugLogBuilder CreateError()
			=> CreateLog(LogType.Error);

		#endregion DebugLogBuilder Utility Methods

		// https://answers.unity.com/questions/289006/catching-double-clicking-console-messages.html
		private static bool TryGetTag(ref string tag)
		{
			string? callerTypeName = null;
			string? methodName = null;

			if (TryGetCallerTypeAndMethodName(ref callerTypeName, ref methodName))
			{
				tag = FormatTag(callerTypeName, methodName);
				return true;
			}
			return false;
		}

		private static bool TryGetCallerTypeAndMethodName(ref string? callerTypeName, ref string? methodName)
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

			if (!string.IsNullOrEmpty(callerTypeName) && !string.IsNullOrEmpty(methodName))
			{
				return true;
			}

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