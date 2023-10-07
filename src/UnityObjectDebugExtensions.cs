using System;
using UnityEngine;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	public static class UnityObjectDebugExtensions
	{
		public static void Log(this UnityObject context, object message)
		{
			Debug.Logger.Log(
				LogType.Log,
				context.GetType().Name,
				message,
				context
				);
		}

		public static void Log(this UnityObject context, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Log,
				context.GetType().Name,
				MessageUtility.Join(message),
				context
				);
		}

		public static void LogWarning(this UnityObject context, object message)
		{
			Debug.Logger.Log(
				LogType.Warning,
				context.GetType().Name,
				message,
				context
				);
		}

		public static void LogWarning(this UnityObject context, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Warning,
				context.GetType().Name,
				MessageUtility.Join(message),
				context
				);
		}

		public static void LogError(this UnityObject context, object message)
		{
			Debug.Logger.Log(
				LogType.Error,
				context.GetType().Name,
				message,
				context
				);
		}

		public static void LogError(this UnityObject context, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Error,
				context.GetType().Name,
				MessageUtility.Join(message),
				context
				);
		}

		public static void LogException(this UnityObject context, Exception exception)
		{
			Debug.Logger.LogException(exception, context);
		}

		public static void Assert(this UnityObject context, bool condition)
		{
			Debug.Assert(condition, context);
		}

		public static void Assert(this UnityObject context, bool condition, object message)
		{
			Debug.Assert(condition, message, context);
		}
	}
}