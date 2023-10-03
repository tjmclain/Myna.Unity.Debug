#if UNITY_5_3_OR_NEWER

using System;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	public static class UnityObjectDebugExtensions
	{
		#region Log Methods

		public static void Log(this UnityObject context, object message)
		{
			Debug.Context(context).Tag(context).Log(message);
		}

		public static void Log(this UnityObject context, object msg0, object msg1)
		{
			Debug.Context(context).Tag(context).Log(msg0, msg1);
		}

		public static void Log(this UnityObject context, object msg0, object msg1, object msg2)
		{
			Debug.Context(context).Tag(context).Log(msg0, msg1, msg2);
		}

		public static void Log(this UnityObject context, object msg0, object msg1, object msg2, object msg3)
		{
			Debug.Context(context).Tag(context).Log(msg0, msg1, msg2, msg3);
		}

		public static void Log(this UnityObject context, params object[] message)
		{
			Debug.Context(context).Tag(context).Log(message);
		}

		#endregion Log Methods

		#region LogWarning Methods

		public static void LogWarning(this UnityObject context, object message)
		{
			Debug.Context(context).Tag(context).LogWarning(message);
		}

		public static void LogWarning(this UnityObject context, object msg0, object msg1)
		{
			Debug.Context(context).Tag(context).LogWarning(msg0, msg1);
		}

		public static void LogWarning(this UnityObject context, object msg0, object msg1, object msg2)
		{
			Debug.Context(context).Tag(context).LogWarning(msg0, msg1, msg2);
		}

		public static void LogWarning(this UnityObject context, object msg0, object msg1, object msg2, object msg3)
		{
			Debug.Context(context).Tag(context).LogWarning(msg0, msg1, msg2, msg3);
		}

		public static void LogWarning(this UnityObject context, params object[] message)
		{
			Debug.Context(context).Tag(context).LogWarning(message);
		}

		#endregion LogWarning Methods

		#region LogError Methods

		public static void LogError(this UnityObject context, object message)
		{
			Debug.Context(context).Tag(context).LogError(message);
		}

		public static void LogError(this UnityObject context, object msg0, object msg1)
		{
			Debug.Context(context).Tag(context).LogError(msg0, msg1);
		}

		public static void LogError(this UnityObject context, object msg0, object msg1, object msg2)
		{
			Debug.Context(context).Tag(context).LogError(msg0, msg1, msg2);
		}

		public static void LogError(this UnityObject context, object msg0, object msg1, object msg2, object msg3)
		{
			Debug.Context(context).Tag(context).LogError(msg0, msg1, msg2, msg3);
		}

		public static void LogError(this UnityObject context, params object[] message)
		{
			Debug.Context(context).Tag(context).LogError(message);
		}

		#endregion LogError Methods

		public static void LogException(this UnityObject context, Exception exception)
		{
			UnityEngine.Debug.LogException(exception, context);
		}
	}
}

#endif