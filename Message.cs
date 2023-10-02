using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Myna.Unity.Debug
{
	public class Message : IDisposable
	{
		private readonly StringBuilder _message = new StringBuilder();

		private const string _separator = "; ";
		private static readonly Queue<Message> _pool = new Queue<Message>();

		public LogType LogType { get; set; } = LogType.Log;
		public string Tag { get; set; } = string.Empty;
		public UnityEngine.Object Context { get; set; } = null;
		public Logger Logger { get; set; } = Debug.Logger;

		public static Message Create(LogType logType)
		{
			if (_pool.Count > 0)
			{
				var log = _pool.Dequeue();
				log.LogType = logType;
				return log;
			}
			else
			{
				return new Message()
				{
					LogType = logType
				};
			}
		}

		public Message Append(string message)
		{
			if (_message.Length > 0)
			{
				_message.Append(_separator);
			}

			_message.Append(message);
			return this;
		}

		public Message AppendIf(string message, bool condition)
		{
			return condition ? Append(message) : this;
		}

		public Message SetTag(string tag)
		{
			Tag = tag;
			return this;
		}

		public Message SetTag(object caller, string methodName)
			=> SetTag(caller.GetType(), methodName);

		public Message SetTag(Type callerType, string methodName)
		{
			Tag = Logger.FormatTag(callerType.Name, methodName);
			return this;
		}

		public void Print()
		{
			if (_message.Length == 0)
			{
				return;
			}

			Logger.Log(this);
		}

		public void Clear()
		{
			_message.Clear();
		}

		#region System.Object

		public override string ToString()
		{
			return _message.ToString();
		}

		#endregion System.Object

		#region IDisposable

		public void Dispose()
		{
			Clear();

			// reset values
			Context = null;
			Logger = Logger.Default;
			LogType = LogType.Log;
			Tag = string.Empty;

			_pool.Enqueue(this);
		}

		#endregion IDisposable
	}
}