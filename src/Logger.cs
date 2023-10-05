using System;
using UnityEngine;

namespace Myna.Unity.Debug
{
	public class Logger
	{
		public LogType FilterLogType { get; set; } = LogType.Log;
		public LogType EditModeFilterLogType { get; set; } = LogType.Log;

		public bool IsLogTypeAllowed(LogType logType)
		{
			var allowedLogType = Application.isPlaying
				? FilterLogType
				: EditModeFilterLogType;

			return (int)logType <= (int)allowedLogType;
		}

		public void Assert(bool condition) => UnityEngine.Debug.Assert(condition);

		public void LogException(Exception exception) => Debug.Logger.LogException(exception);

		#region Log Methods

		public void Log(object message) => Log(LogType.Log, message);

		public void LogFormat(string format, params object[] args) => Log(LogType.Log, string.Format(format, args));

		public void LogWarning(object message) => Log(LogType.Warning, message);

		public void LogWarningFormat(string format, params object[] args) => Log(LogType.Warning, string.Format(format, args));

		public void LogError(object message) => Log(LogType.Error, message);

		public void LogErrorFormat(string format, params object[] args) => Log(LogType.Warning, string.Format(format, args));

		public void Log(LogType logType, object message, string tag = "", UnityEngine.Object context = null)
		{
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			Debug.Logger.Log(logType, tag, message, context);
		}

		#endregion Log Methods
	}
}