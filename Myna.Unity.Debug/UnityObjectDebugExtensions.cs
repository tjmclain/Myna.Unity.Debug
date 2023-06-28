using System;
using System.Collections.Generic;
using System.Text;

namespace Myna.Unity.Debug
{
	public static class UnityObjectDebugExtensions
	{
		public static void Log(this UnityEngine.Object self, object message)
			=> Log(self, UnityEngine.LogType.Log, message);

		public static void LogWarning(this UnityEngine.Object self, object message)
			=> Log(self, UnityEngine.LogType.Warning, message);

		public static void LogError(this UnityEngine.Object self, object message)
			=> Log(self, UnityEngine.LogType.Error, message);

		private static void Log(UnityEngine.Object self, UnityEngine.LogType logType, object message)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = self;
				log.Append(message.ToString());
				log.Print();
			}
		}

		public static void Log(this UnityEngine.Object self, string a, string b)
			=> Log(self, UnityEngine.LogType.Log, a, b);

		public static void LogWarning(this UnityEngine.Object self, string a, string b)
			=> Log(self, UnityEngine.LogType.Warning, a, b);

		public static void LogError(this UnityEngine.Object self, string a, string b)
			=> Log(self, UnityEngine.LogType.Error, a, b);

		private static void Log(UnityEngine.Object self, UnityEngine.LogType logType, string a, string b)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = self;
				log.Append(a);
				log.Append(b);
				log.Print();
			}
		}

		public static void Log(this UnityEngine.Object self, string a, string b, string c)
			=> Log(self, UnityEngine.LogType.Log, a, b, c);

		public static void LogWarning(this UnityEngine.Object self, string a, string b, string c)
			=> Log(self, UnityEngine.LogType.Warning, a, b, c);

		public static void LogError(this UnityEngine.Object self, string a, string b, string c)
			=> Log(self, UnityEngine.LogType.Error, a, b, c);

		private static void Log(UnityEngine.Object self, UnityEngine.LogType logType, string a, string b, string c)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = self;
				log.Append(a);
				log.Append(b);
				log.Append(c);
				log.Print();
			}
		}

		public static void Log(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Log(self, UnityEngine.LogType.Log, a, b, c, d);

		public static void LogWarning(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Log(self, UnityEngine.LogType.Warning, a, b, c, d);

		public static void LogError(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Log(self, UnityEngine.LogType.Error, a, b, c, d);

		private static void Log(UnityEngine.Object self, UnityEngine.LogType logType, string a, string b, string c, string d)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = self;
				log.Append(a);
				log.Append(b);
				log.Append(c);
				log.Append(d);
				log.Print();
			}
		}

		public static void Log(this UnityEngine.Object self, params string[] messages)
			=> Log(self, UnityEngine.LogType.Log, messages);

		public static void LogWarning(this UnityEngine.Object self, params string[] messages)
			=> Log(self, UnityEngine.LogType.Warning, messages);

		public static void LogError(this UnityEngine.Object self, params string[] messages)
			=> Log(self, UnityEngine.LogType.Error, messages);

		private static void Log(UnityEngine.Object self, UnityEngine.LogType logType, params string[] messages)
		{
			using (var log = DebugLogBuilder.Create(logType))
			{
				log.Context = self;
				foreach (string message in messages)
				{
					log.Append(message);
				}
				log.Print();
			}
		}
	}
}
