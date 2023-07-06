using System;
using LogType = UnityEngine.LogType;

namespace Myna.Unity.Debug
{
	// static wrapper for Logger calls
	public static class Debug
	{
		public static readonly Logger Logger = new Logger();

		public static void Log(string message)
			=> Logger.Log(message);

		public static void Log(LogType logType, string message)
			=> Logger.Log(logType, message);

		#region Mirroring Methods from UnityEngine.Debug

		public static void Log(LogType logType, object message)
			=> Logger.Log(logType, message);

		public static void Log(LogType logType, object message, UnityEngine.Object context)
			=> Logger.Log(logType, message, context);

		public static void Log(object message)
			=> Logger.Log(LogType.Log, message);

		public static void LogFormat(string format, params object[] args)
			=> Logger.LogFormat(LogType.Log, format, args);

		public static void LogWarning(object message)
			=> Logger.Log(LogType.Warning, message);

		public static void LogWarningFormat(string format, params object[] args)
			=> Logger.LogFormat(LogType.Warning, format, args);

		public static void LogError(object message)
			=> Logger.Log(LogType.Error, message);

		public static void LogErrorFormat(string format, params object[] args)
			=> Logger.LogFormat(LogType.Error, format, args);

		public static void LogException(Exception exception)
			=> Logger.LogException(exception);

		public static void Assert(bool condition)
			=> UnityEngine.Debug.Assert(condition);

		#endregion Mirroring Methods from UnityEngine.Debug

		#region Conditional Methods

		public static void LogIf(object message, Func<bool> condition)
			=> LogIf(message, condition.Invoke());

		public static void LogIf(object message, bool condition)
		{
			if (condition)
			{
				Logger.Log(LogType.Log, message);
			}
		}

		public static void LogWarningIf(object message, Func<bool> condition)
			=> LogWarningIf(message, condition.Invoke());

		public static void LogWarningIf(object message, bool condition)
		{
			if (condition)
			{
				Logger.Log(LogType.Warning, message);
			}
		}

		public static void LogErrorIf(object message, Func<bool> condition)
			=> LogErrorIf(message, condition.Invoke());

		public static void LogErrorIf(object message, bool condition)
		{
			if (condition)
			{
				Logger.Log(LogType.Warning, message);
			}
		}

		#endregion Conditional Methods

		#region Log Concatenation Methods

		public static void Log(LogType logType, string a, string b)
			=> Log(null, logType, a, b);

		public static void Log(string a, string b)
			=> Log(null, LogType.Log, a, b);

		public static void LogWarning(string a, string b)
			=> Log(null, LogType.Warning, a, b);

		public static void LogError(string a, string b)
			=> Log(null, LogType.Error, a, b);

		public static void Log(UnityEngine.Object? context, LogType logType, string a, string b)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = context;
				log.Append(a);
				log.Append(b);
				log.Print();
			}
		}

		public static void Log(LogType logType, string a, string b, string c)
			=> Log(null, logType, a, b, c);

		public static void Log(string a, string b, string c)
			=> Log(null, LogType.Log, a, b, c);

		public static void LogWarning(string a, string b, string c)
			=> Log(null, LogType.Warning, a, b, c);

		public static void LogError(string a, string b, string c)
			=> Log(null, LogType.Error, a, b, c);

		public static void Log(UnityEngine.Object? context, LogType logType, string a, string b, string c)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = context;
				log.Append(a);
				log.Append(b);
				log.Append(c);
				log.Print();
			}
		}

		public static void Log(LogType logType, string a, string b, string c, string d)
			=> Log(null, logType, a, b, c, d);

		public static void Log(string a, string b, string c, string d)
			=> Log(null, LogType.Log, a, b, c, d);

		public static void LogWarning(string a, string b, string c, string d)
			=> Log(null, LogType.Warning, a, b, c, d);

		public static void LogError(string a, string b, string c, string d)
			=> Log(null, LogType.Error, a, b, c, d);

		public static void Log(UnityEngine.Object? context, LogType logType, string a, string b, string c, string d)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = context;
				log.Append(a);
				log.Append(b);
				log.Append(c);
				log.Append(d);
				log.Print();
			}
		}

		public static void Log(LogType logType, params string[] messages)
			=> Log(null, logType, messages);

		public static void Log(UnityEngine.Object? context, LogType logType, params string[] messages)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = context;
				foreach (string message in messages)
				{
					log.Append(message);
				}
				log.Print();
			}
		}

		#endregion Log Concatenation Methods

		#region Logger

		public static Logger CreateLogger()
		{
			return new Logger();
		}

		#endregion Logger

		#region DebugLogBuilder

		public static DebugLogBuilder CreateLog()
			=> DebugLogBuilder.Create(LogType.Log);

		public static DebugLogBuilder CreateWarning()
			=> DebugLogBuilder.Create(LogType.Warning);

		public static DebugLogBuilder CreateError()
			=> DebugLogBuilder.Create(LogType.Error);

		#endregion DebugLogBuilder
	}
}