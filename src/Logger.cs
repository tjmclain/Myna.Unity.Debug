using System;
using System.Diagnostics;
using UnityEngine;

namespace Myna.Unity.Debug
{
	public class Logger
	{
		public LogType FilterLogType { get; set; } = LogType.Log;
		public LogType EditModeFilterLogType { get; set; } = LogType.Log;
		public string Tag { get; set; } = string.Empty;
		public UnityEngine.Object Owner { get; set; } = null;

		public bool IsLogTypeAllowed(LogType logType)
		{
			var allowedLogType = Application.isPlaying
				? FilterLogType
				: EditModeFilterLogType;

			return (int)logType <= (int)allowedLogType;
		}

		#region Public Debug Methods

		public void Log(object message) => LogInternal(LogType.Log, message);

		public void Log(params object[] message) => LogInternal(LogType.Log, MessageUtility.Join(message));

		public void LogWarning(object message) => LogInternal(LogType.Warning, message);

		public void LogWarning(params object[] message) => LogInternal(LogType.Warning, MessageUtility.Join(message));

		public void LogError(object message) => LogInternal(LogType.Error, message);

		public void LogError(params object[] message) => LogInternal(LogType.Error, MessageUtility.Join(message));

		public void LogException(Exception exception) => Debug.Logger.LogException(exception);

		public void Assert(bool condition) => Debug.Assert(condition);

		#endregion Public Debug Methods

		private void LogInternal(LogType logType, object message)
		{
			if (!IsLogTypeAllowed(logType))
			{
				return;
			}

			string tag = Tag;
			TagAutoIfTagIsUnset(ref tag);

			Debug.Logger.Log(logType, tag, message, Owner);
		}

		[Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
		private void TagAutoIfTagIsUnset(ref string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				tag = TagUtility.GetDefaultTag();
			}
		}
	}
}