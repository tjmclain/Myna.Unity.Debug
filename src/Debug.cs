#if UNITY_5_3_OR_NEWER

using System;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	// static wrapper for Logger calls
	public static class Debug
	{
		public static readonly Logger Logger = new Logger();

		#region UnityEngine.Debug

		public static void Log(object message) => Logger.Log(message);

		public static void LogFormat(string format, params object[] args) => Logger.LogFormat(format, args);

		public static void LogWarning(object message) => Logger.LogWarning(message);

		public static void LogWarningFormat(string format, params object[] args) => Logger.LogWarningFormat(format, args);

		public static void LogError(object message) => Logger.LogError(message);

		public static void LogErrorFormat(string format, params object[] args) => Logger.LogErrorFormat(format, args);

		public static void LogException(Exception exception) => Logger.LogException(exception);

		public static void Assert(bool condition) => Logger.Assert(condition);

		#endregion UnityEngine.Debug

		#region LogInfo / Properties

		public static LogInfo Tag(string tag) => LogInfo.Get().Tag(tag);

		public static LogInfo Tag(object caller) => LogInfo.Get().Tag(caller);

		public static LogInfo Tag(object caller, string methodName) => LogInfo.Get().Tag(caller, methodName);

		public static LogInfo TagAuto() => LogInfo.Get().TagAuto();

		public static LogInfo Context(UnityObject context) => LogInfo.Get().Context(context);

		public static LogInfo If(bool condition) => LogInfo.Get().If(condition);

		public static LogInfo If(Func<bool> condition) => LogInfo.Get().If(condition);

		public static LogInfo If<T>(Predicate<T> condition, T obj) => LogInfo.Get().If(condition, obj);

		#endregion LogInfo / Properties
	}
}

#endif