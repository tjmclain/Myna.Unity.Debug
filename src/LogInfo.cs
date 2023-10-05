using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	public class LogInfo
	{
		private const string _messageSeparator = " ";
		private static readonly Queue<LogInfo> _pool = new Queue<LogInfo>();

		private readonly List<object> _message = new List<object>(4);
		private string _tag = string.Empty;
		private UnityObject _context = null;

		public static LogInfo Get() => _pool.TryDequeue(out var logInfo) ? logInfo : new LogInfo();

		public object GetMessage() => string.Join(_messageSeparator, _message);

		#region Property Setters

		public LogInfo Tag(string tag)
		{
			_tag = tag;
			return this;
		}

		public LogInfo Tag(object caller)
		{
			_tag = caller.GetType().Name;
			return this;
		}

		public LogInfo Tag(object caller, string methodName)
		{
			_tag = TagUtility.FormatTag(caller, methodName);
			return this;
		}

		public LogInfo TagAuto()
		{
			_tag = TagUtility.GetDefaultTag();
			return this;
		}

		public LogInfo Context(UnityObject context)
		{
			_context = context;
			return this;
		}

		#endregion Property Setters

		#region Log

		public void Log(object message) => Log(LogType.Log, message);

		public void Log(object msg0, object msg1) => Log(LogType.Log, msg0, msg1);

		public void Log(object msg0, object msg1, object msg2) => Log(LogType.Log, msg0, msg1, msg2);

		public void Log(object msg0, object msg1, object msg2, object msg3) => Log(LogType.Log, msg0, msg1, msg2, msg3);

		public void Log(params object[] message) => Log(LogType.Log, message);

		#endregion Log

		#region LogWarning

		public void LogWarning(object message) => Log(LogType.Warning, message);

		public void LogWarning(object msg0, object msg1) => Log(LogType.Warning, msg0, msg1);

		public void LogWarning(object msg0, object msg1, object msg2) => Log(LogType.Warning, msg0, msg1, msg2);

		public void LogWarning(object msg0, object msg1, object msg2, object msg3) => Log(LogType.Warning, msg0, msg1, msg2, msg3);

		public void LogWarning(params object[] message) => Log(LogType.Warning, message);

		#endregion LogWarning

		#region LogError

		public void LogError(object message) => Log(LogType.Error, message);

		public void LogError(object msg0, object msg1) => Log(LogType.Error, msg0, msg1);

		public void LogError(object msg0, object msg1, object msg2) => Log(LogType.Error, msg0, msg1, msg2);

		public void LogError(object msg0, object msg1, object msg2, object msg3) => Log(LogType.Error, msg0, msg1, msg2, msg3);

		public void LogError(params object[] message) => Log(LogType.Error, message);

		#endregion LogError

		#region Private Log Methods

		private void Log(LogType logType, object message)
		{
			_message.Add(message);

			// print log and release back to pool
			Print(logType);
			Release();
		}

		private void Log(LogType logType, object msg0, object msg1)
		{
			_message.Add(msg0);
			_message.Add(msg1);

			// print log and release back to pool
			Print(logType);
			Release();
		}

		private void Log(LogType logType, object msg0, object msg1, object msg2)
		{
			_message.Add(msg0);
			_message.Add(msg1);
			_message.Add(msg2);

			// print log and release back to pool
			Print(logType);
			Release();
		}

		private void Log(LogType logType, object msg0, object msg1, object msg2, object msg3)
		{
			_message.Add(msg0);
			_message.Add(msg1);
			_message.Add(msg2);
			_message.Add(msg3);

			// print log and release back to pool
			Print(logType);
			Release();
		}

		private void Log(LogType logType, object[] message)
		{
			_message.AddRange(message);

			// print log and release back to pool
			Print(logType);
			Release();
		}

		#endregion Private Log Methods

		private void Print(LogType logType)
		{
			TagAutoIfTagIsUnset();
			Debug.Logger.Log(logType, _tag, GetMessage(), _context);
		}

		private void Release()
		{
			// reset members to defaults
			_message.Clear();
			_tag = string.Empty;
			_context = null;

			// release to pool
			_pool.Enqueue(this);
		}

		[Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
		private void TagAutoIfTagIsUnset()
		{
			if (string.IsNullOrEmpty(_tag))
			{
				TagAuto();
			}
		}
	}
}