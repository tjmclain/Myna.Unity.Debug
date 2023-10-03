#if UNITY_5_3_OR_NEWER

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	public class LogInfo
	{
		private const string _separator = " ";
		private static readonly Queue<LogInfo> _pool = new Queue<LogInfo>();

		private readonly List<object> _message = new List<object>(4);
		private string _tag = string.Empty;
		private UnityObject _context = null;
		private Logger _logger = Debug.Logger;

		public LogType LogType { get; private set; } = LogType.Log;
		public bool Invalid { get; private set; } = false;

		public static LogInfo Get()
		{
			return _pool.TryDequeue(out var logInfo) ? logInfo : new LogInfo();
		}

		#region Property Getters

		public object GetMessage() => string.Join(_separator, _message);

		public string GetTag() => _tag;

		public UnityObject GetContext() => _context;

		#endregion Property Getters

		#region Property Setters

		public LogInfo Logger(Logger logger)
		{
			_logger = logger;
			return this;
		}

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

		public LogInfo If(bool condition)
		{
			Invalid = !condition;
			return this;
		}

		public LogInfo If(Func<bool> condition)
		{
			Invalid = !condition();
			return this;
		}

		public LogInfo If<T>(Predicate<T> condition, T obj)
		{
			Invalid = !condition(obj);
			return this;
		}

		#endregion Property Setters

		#region Log

		public void Log(object message)
		{
			LogType = LogType.Log;
			Print(message);
		}

		public void Log(object msg0, object msg1)
		{
			LogType = LogType.Log;
			Print(msg0, msg1);
		}

		public void Log(object msg0, object msg1, object msg2)
		{
			LogType = LogType.Log;
			Print(msg0, msg1, msg2);
		}

		public void Log(object msg0, object msg1, object msg2, object msg3)
		{
			LogType = LogType.Log;
			Print(msg0, msg1, msg2, msg3);
		}

		public void Log(params object[] message)
		{
			LogType = LogType.Log;
			Print(message);
		}

		#endregion Log

		#region LogWarning

		public void LogWarning(object message)
		{
			LogType = LogType.Warning;
			Print(message);
		}

		public void LogWarning(object msg0, object msg1)
		{
			LogType = LogType.Warning;
			Print(msg0, msg1);
		}

		public void LogWarning(object msg0, object msg1, object msg2)
		{
			LogType = LogType.Warning;
			Print(msg0, msg1, msg2);
		}

		public void LogWarning(object msg0, object msg1, object msg2, object msg3)
		{
			LogType = LogType.Warning;
			Print(msg0, msg1, msg2, msg3);
		}

		public void LogWarning(params object[] message)
		{
			LogType = LogType.Warning;
			Print(message);
		}

		#endregion LogWarning

		#region LogError

		public void LogError(object message)
		{
			LogType = LogType.Error;
			Print(message);
		}

		public void LogError(object msg0, object msg1)
		{
			LogType = LogType.Error;
			Print(msg0, msg1);
		}

		public void LogError(object msg0, object msg1, object msg2)
		{
			LogType = LogType.Error;
			Print(msg0, msg1, msg2);
		}

		public void LogError(object msg0, object msg1, object msg2, object msg3)
		{
			LogType = LogType.Error;
			Print(msg0, msg1, msg2, msg3);
		}

		public void LogError(params object[] message)
		{
			LogType = LogType.Error;
			Print(message);
		}

		#endregion LogError

		#region Print Methods

		private void Print(object message)
		{
			_message.Add(message);

			PrintAndRelease();
		}

		private void Print(object msg0, object msg1)
		{
			_message.Add(msg0);
			_message.Add(msg1);

			PrintAndRelease();
		}

		private void Print(object msg0, object msg1, object msg2)
		{
			_message.Add(msg0);
			_message.Add(msg1);
			_message.Add(msg2);

			PrintAndRelease();
		}

		private void Print(object msg0, object msg1, object msg2, object msg3)
		{
			_message.Add(msg0);
			_message.Add(msg1);
			_message.Add(msg2);
			_message.Add(msg3);

			PrintAndRelease();
		}

		private void Print(object[] message)
		{
			_message.AddRange(message);

			PrintAndRelease();
		}

		private void PrintAndRelease()
		{
			// print log
			_logger.Log(this);

			// reset members to defaults
			_message.Clear();
			_tag = string.Empty;
			_context = null;
			_logger = Debug.Logger;

			LogType = LogType.Log;

			// release to pool
			_pool.Enqueue(this);
		}

		#endregion Print Methods
	}
}

#endif