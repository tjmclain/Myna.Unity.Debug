using System;
using LogType = UnityEngine.LogType;

namespace Myna.Unity.Debug
{
	// static wrapper for Logger calls
	public static class Debug
	{
		public static readonly Logger Logger = new Logger();

		#region Mirroring Methods from UnityEngine.Debug
		public static void Log(LogType logType, object message)
			=> Logger.Log(logType, message);

		public static void Log(object message) =>
			Logger.Log(LogType.Log, message);

		public static void LogFormat(string format, params object[] args) =>
			Logger.LogFormat(LogType.Log, format, args);

		public static void LogWarning(object message) =>
			Logger.Log(LogType.Warning, message);

		public static void LogWarningFormat(string format, params object[] args) =>
			Logger.LogFormat(LogType.Warning, format, args);

		public static void LogError(object message) =>
			Logger.Log(LogType.Error, message);

		public static void LogErrorFormat(string format, params object[] args) =>
			Logger.LogFormat(LogType.Error, format, args);

		public static void LogException(Exception exception) =>
			Logger.LogException(exception);

		public static void Assert(bool condition) =>
			UnityEngine.Debug.Assert(condition);
		#endregion

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
		#endregion

		#region Logger
		public static Logger CreateLogger()
		{
			return new Logger();
		}
		#endregion

		#region DebugLogBuilder
		public static DebugLogBuilder CreateLog()
			=> DebugLogBuilder.Create(LogType.Log);

		public static DebugLogBuilder CreateWarning()
			=> DebugLogBuilder.Create(LogType.Warning);

		public static DebugLogBuilder CreateError()
			=> DebugLogBuilder.Create(LogType.Error);
		#endregion
	}
}