using System;
using UnityEngine;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	// static wrapper for Logger calls
	public static class Debug
	{
		public static readonly Logger Logger = new Logger();

		#region UnityEngine.Debug

		public static void Log(object message)
			=> Logger.Log(LogType.Log, message);

		public static void LogFormat(string format, params object[] args)
			=> Logger.LogFormat(format, args);

		public static void LogWarning(object message)
			=> Logger.Log(LogType.Warning, message);

		public static void LogWarningFormat(string format, params object[] args)
			=> Logger.LogFormat(format, args);

		public static void LogError(object message)
			=> Logger.Log(LogType.Error, message);

		public static void LogErrorFormat(string format, params object[] args)
			=> Logger.LogFormat(format, args);

		public static void LogException(Exception exception)
			=> Logger.LogException(exception);

		public static void Assert(bool condition)
			=> UnityEngine.Debug.Assert(condition);

		#endregion UnityEngine.Debug

		#region LogInfo

		public static LogInfo Tag(string tag) => LogInfo.Get().Tag(tag);

		public static LogInfo Tag(object caller) => LogInfo.Get().Tag(caller);

		public static LogInfo Tag(object caller, string methodName) => LogInfo.Get().Tag(caller, methodName);

		public static LogInfo TagAuto() => LogInfo.Get().TagAuto();

		public static LogInfo Context(UnityObject context) => LogInfo.Get().Context(context);

		public static LogInfo If(bool condition) => LogInfo.Get().If(condition);

		public static LogInfo If(Func<bool> condition) => LogInfo.Get().If(condition);

		public static LogInfo If<T>(Predicate<T> condition, T obj) => LogInfo.Get().If(condition, obj);

		#endregion LogInfo
	}
}