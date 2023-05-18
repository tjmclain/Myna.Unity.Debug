#if DEBUG || DEVELOPMENT_BUILD || UNITY_EDITOR
#define CONSOLE_ENABLED
#endif

#if CONSOLE_ENABLED
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
#endif

using UnityEngine;

namespace Myna.Unity.Debug
{
	// In order to navigate to where this call was made from, 
	// I need to export my debug scripts as a separate DLL: 
	// https://answers.unity.com/questions/289006/catching-double-clicking-console-messages.html
	public static class Console
	{
		[Serializable]
		public struct Log
		{
			public string Message;
			public string StackTrace;
			public LogType LogType;
		}

#if CONSOLE_ENABLED
		internal class LogHandler : ILogHandler
		{
			private static readonly StackTrace _stackTrace = new();
			private static readonly ILogHandler _defaultLogHandler = UnityEngine.Debug.unityLogger.logHandler;

			private readonly StringBuilder _sb = new();

			public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
			{
				static bool TryGetCallingFrame(out StackFrame? frame)
				{
					if (_stackTrace.FrameCount < 3)
					{
						frame = null;
						return false;
					}

					// Not sure why, but index 2 is the correct level
					int index = 2;
					frame = _stackTrace.GetFrame(index);
					return true;
				}

				if (TryGetCallingFrame(out var frame))
				{
					var methodInfo = frame?.GetMethod();
					var typeInfo = methodInfo?.DeclaringType;

					_sb.Clear();
					if (typeInfo != null)
					{
						_sb.Append('[');
						_sb.Append(typeInfo.Name);
						_sb.Append("] ");
					}

					if (methodInfo != null)
					{
						_sb.Append(methodInfo.Name);
						_sb.Append(": ");
					}

					_sb.Append(format);
					format = _sb.ToString();
				}

				_defaultLogHandler.LogFormat(logType, context, format, args);
			}

			public void LogException(Exception exception, UnityEngine.Object context)
			{
				_defaultLogHandler.LogException(exception, context);
			}
		}

		private readonly static List<Log> _logs = new();

		[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Initialize()
		{
			_logs.Clear();

			Application.logMessageReceived -= OnLogMessageReceived;
			Application.logMessageReceived += OnLogMessageReceived;

			UnityEngine.Debug.unityLogger.logHandler = new LogHandler();
		}

		private static void OnLogMessageReceived(string message, string stackTrace, LogType logType)
		{
			_logs.Add(new Log()
			{
				Message = message,
				StackTrace = stackTrace,
				LogType = logType
			});
		}
#endif

		public static Log[] GetLogs()
		{
#if CONSOLE_ENABLED
			return _logs.ToArray();
#else
			return Array.Empty<Log>();
#endif
		}

		#region Public interface
		[Conditional("CONSOLE_ENABLED")]
		public static void ClearLogs()
		{
			_logs.Clear();
		}

		[Conditional("CONSOLE_ENABLED")]
		public static void WriteLogsToFile(string path)
		{
			string json = JsonUtility.ToJson(_logs);
			File.WriteAllText(path, json);
		}

		[Conditional("CONSOLE_ENABLED")]
		public static void OnGUI()
		{


		}
		#endregion
	}
}