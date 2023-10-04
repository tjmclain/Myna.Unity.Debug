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
			=> Logger.Log(message);

		public static void LogFormat(string format, params object[] args)
			=> Logger.LogFormat(format, args);

		public static void LogWarning(object message)
			=> Logger.LogWarning(message);

		public static void LogWarningFormat(string format, params object[] args)
			=> Logger.LogWarningFormat(format, args);

		public static void LogError(object message)
			=> Logger.LogError(message);

		public static void LogErrorFormat(string format, params object[] args)
			=> Logger.LogErrorFormat(format, args);

		public static void LogException(Exception exception)
			=> UnityEngine.Debug.LogException(exception);

		public static void Assert(bool condition)
			=> UnityEngine.Debug.Assert(condition);

		#endregion UnityEngine.Debug

		#region LogInfo Property Setters

		public static LogInfo Tag(string tag)
			=> LogInfo.Get().Tag(tag);

		public static LogInfo Tag(object caller)
			=> LogInfo.Get().Tag(caller);

		public static LogInfo Tag(object caller, string methodName)
			=> LogInfo.Get().Tag(caller, methodName);

		public static LogInfo TagAuto()
			=> LogInfo.Get().TagAuto();

		public static LogInfo Context(UnityObject context)
			=> LogInfo.Get().Context(context);

		#endregion LogInfo Property Setters

		#region LogInfo.Log

		public static void Log(object msg0, object msg1)
			=> LogInfo.Get().Log(msg0, msg1);

		public static void Log(object msg0, object msg1, object msg2)
			=> LogInfo.Get().Log(msg0, msg1, msg2);

		public static void Log(object msg0, object msg1, object msg2, object msg3)
			=> LogInfo.Get().Log(msg0, msg1, msg2, msg3);

		public static void Log(params object[] message)
			=> LogInfo.Get().Log(message);

		#endregion LogInfo.Log

		#region LogInfo.LogWarning

		public static void LogWarning(object msg0, object msg1)
			=> LogInfo.Get().LogWarning(msg0, msg1);

		public static void LogWarning(object msg0, object msg1, object msg2)
			=> LogInfo.Get().LogWarning(msg0, msg1, msg2);

		public static void LogWarning(object msg0, object msg1, object msg2, object msg3)
			=> LogInfo.Get().LogWarning(msg0, msg1, msg2, msg3);

		public static void LogWarning(params object[] message)
			=> LogInfo.Get().LogWarning(message);

		#endregion LogInfo.LogWarning

		#region LogInfo.LogError

		public static void LogError(object msg0, object msg1)
			=> LogInfo.Get().LogError(msg0, msg1);

		public static void LogError(object msg0, object msg1, object msg2)
			=> LogInfo.Get().LogError(msg0, msg1, msg2);

		public static void LogError(object msg0, object msg1, object msg2, object msg3)
			=> LogInfo.Get().LogError(msg0, msg1, msg2, msg3);

		private static void LogError(params object[] message)
			=> LogInfo.Get().LogError(message);

		#endregion LogInfo.LogError
	}
}