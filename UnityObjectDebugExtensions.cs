namespace Myna.Unity.Debug
{
	public static class UnityObjectDebugExtensions
	{
		public static void Log(this UnityEngine.Object self, object message)
			=> Debug.Log(UnityEngine.LogType.Log, message, self);

		public static void LogWarning(this UnityEngine.Object self, object message)
			=> Debug.Log(UnityEngine.LogType.Warning, message, self);

		public static void LogError(this UnityEngine.Object self, object message)
			=> Debug.Log(UnityEngine.LogType.Error, message, self);

		public static void Log(this UnityEngine.Object self, string a, string b)
			=> Debug.Log(self, UnityEngine.LogType.Log, a, b);

		public static void LogWarning(this UnityEngine.Object self, string a, string b)
			=> Debug.Log(self, UnityEngine.LogType.Warning, a, b);

		public static void LogError(this UnityEngine.Object self, string a, string b)
			=> Debug.Log(self, UnityEngine.LogType.Error, a, b);

		public static void Log(this UnityEngine.Object self, string a, string b, string c)
			=> Debug.Log(self, UnityEngine.LogType.Log, a, b, c);

		public static void LogWarning(this UnityEngine.Object self, string a, string b, string c)
			=> Debug.Log(self, UnityEngine.LogType.Warning, a, b, c);

		public static void LogError(this UnityEngine.Object self, string a, string b, string c)
			=> Debug.Log(self, UnityEngine.LogType.Error, a, b, c);

		public static void Log(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Debug.Log(self, UnityEngine.LogType.Log, a, b, c, d);

		public static void LogWarning(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Debug.Log(self, UnityEngine.LogType.Warning, a, b, c, d);

		public static void LogError(this UnityEngine.Object self, string a, string b, string c, string d)
			=> Debug.Log(self, UnityEngine.LogType.Error, a, b, c, d);

		public static void Log(this UnityEngine.Object self, params string[] messages)
			=> Debug.Log(self, UnityEngine.LogType.Log, messages);

		public static void LogWarning(this UnityEngine.Object self, params string[] messages)
			=> Debug.Log(self, UnityEngine.LogType.Warning, messages);

		public static void LogError(this UnityEngine.Object self, params string[] messages)
			=> Debug.Log(self, UnityEngine.LogType.Error, messages);
	}
}