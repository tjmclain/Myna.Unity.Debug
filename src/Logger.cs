#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;

namespace Myna.Unity.Debug
{
	public class Logger
	{
		private static readonly ILogger _unityLogger = UnityEngine.Debug.unityLogger;

		public LogType FilterLogType { get; set; } = LogType.Log;
		public LogType EditModeFilterLogType { get; set; } = LogType.Log;

		public bool IsLogTypeAllowed(LogType logType)
		{
			var allowedLogType = Application.isPlaying
				? FilterLogType
				: EditModeFilterLogType;

			return (int)logType <= (int)allowedLogType;
		}

		public void Assert(bool condition)
		{
			UnityEngine.Debug.Assert(condition);
		}

		public void LogException(Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
		}

		#region Log Methods

		public void Log(object message)
		{
			Log(LogType.Log, message);
		}

		public void LogFormat(string format, params object[] args)
		{
			Log(LogType.Log, string.Format(format, args));
		}

		public void LogWarning(object message)
		{
			Log(LogType.Warning, message);
		}

		public void LogWarningFormat(string format, params object[] args)
		{
			Log(LogType.Warning, string.Format(format, args));
		}

		public void LogError(object message)
		{
			Log(LogType.Error, message);
		}

		public void LogErrorFormat(string format, params object[] args)
		{
			Log(LogType.Warning, string.Format(format, args));
		}

		public void Log(LogInfo log)
		{
			if (log.Invalid)
			{
				return;
			}

			Log(log.LogType, log.GetMessage(), log.GetTag(), log.GetContext());
		}

		public void Log(LogType logType, object message, string tag = "", UnityEngine.Object context = null)
		{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			if (string.IsNullOrEmpty(tag))
			{
				tag = TagUtility.GetDefaultTag();
			}
#endif

			_unityLogger.Log(logType, tag, message, context);
		}

		#endregion Log Methods
	}
}

#endif