#if DEBUG || DEVELOPMENT_BUILD || UNITY_EDITOR
#define USE_CUSTOM_LOGGER
#endif

#if USE_CUSTOM_LOGGER
using System.Diagnostics;
using System.Text;
#endif

using UnityEngine;

namespace Myna.Unity.Debug
{
	public static class Console
	{
#if USE_CUSTOM_LOGGER
		private readonly static StackTrace _stackTrace = new();
		private readonly static StringBuilder _sb = new();
#endif

		// In order to navigate to where this call was made from, 
		// I need to export my debug scripts as a separate DLL: 
		// https://answers.unity.com/questions/289006/catching-double-clicking-console-messages.html
		public static void Log(string message) => Log(LogType.Log, message);
		public static void LogWarning(string message) => Log(LogType.Warning, message);
		public static void LogError(string message) => Log(LogType.Error, message);

		private static void Log(LogType logType, string message)
		{
#if USE_CUSTOM_LOGGER
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

				_sb.Append(message);
				message = _sb.ToString();
			}
#endif

			UnityEngine.Debug.unityLogger.Log(logType, message);
		}
	}
}