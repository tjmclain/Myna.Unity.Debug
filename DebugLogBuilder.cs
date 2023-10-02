using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Myna.Unity.Debug
{
	public class DebugLogBuilder : IDisposable
	{
		private LogType _logType;
		public LogType LogType
		{
			get => _logType;
			set => _logType = value;
		}

		private string? _callerTypeName = null;
		public string? CallerTypeName
		{
			get => _callerTypeName;
			set => _callerTypeName = value;
		}

		private string? _methodName = null;
		public string? MethodName
		{
			get => _methodName;
			set => _methodName = value;
		}

		private UnityEngine.Object? _context = null;
		public UnityEngine.Object? Context
		{
			get => _context;
			set => _context = value;
		}

		private Logger _logger = new Logger();
		public Logger Logger
		{
			get => _logger;
			set => _logger = value;
		}

		private readonly StringBuilder _message = new StringBuilder();
		public string Message => _message.ToString();

		private const string _separator = "; ";

		private static readonly Queue<DebugLogBuilder> _pool = new Queue<DebugLogBuilder>();

		public DebugLogBuilder(LogType logType = LogType.Log)
		{
			_logType = logType;
		}

		public static DebugLogBuilder CreateLog()
			=> Create(LogType.Log);

		public static DebugLogBuilder CreateWarning()
			=> Create(LogType.Warning);

		public static DebugLogBuilder CreateError()
			=> Create(LogType.Error);

		public static DebugLogBuilder Create(LogType logType)
		{
			if (_pool.Count > 0)
			{
				var log = _pool.Dequeue();
				log.LogType = logType;
				return log;
			}
			else
			{
				return new DebugLogBuilder(logType);
			}
		}

		public static void Print(string message)
			=> Print(LogType.Log, message);

		public static void Print(LogType logType, string message)
		{
			using (var log = Create(logType))
			{
				log.Append(message);
				log.Print();
			}
		}

		public static void PrintIf(string message, bool condition)
			=> PrintIf(LogType.Log, message, condition);

		public static void PrintIf(LogType logType, string message, bool condition)
		{
			if (condition)
			{
				Print(logType, message);
			}
		}

		public DebugLogBuilder SetMethodName(string methodName)
		{
			MethodName = methodName;
			return this;
		}

		public DebugLogBuilder Append(string message)
		{
			if (_message.Length > 0)
			{
				_message.Append(_separator);
			}

			_message.Append(message);
			return this;
		}

		public DebugLogBuilder AppendIf(string message, bool condition)
		{
			return condition ? Append(message) : this;
		}

		public DebugLogBuilder AppendFormat(string format, params object[] args)
			=> Append(string.Format(format, args));

		public DebugLogBuilder AppendFormatIf(string format, bool condition, params object[] args)
		{
			return condition ? AppendFormat(format, args) : this;
		}

		public void Print()
		{
			if (_message.Length == 0)
			{
				return;
			}

			_logger.Log(this);
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
		#endregion

		#region IDisposable
		public void Dispose()
		{
			Clear();

			// reset values
			_logType = LogType.Log;
			_logger = Logger.Default;
			_callerTypeName = string.Empty;
			_methodName = string.Empty;

			_pool.Enqueue(this);
		}
		#endregion
	}
}


