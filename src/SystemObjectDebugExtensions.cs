namespace Myna.Unity.Debug
{
	using LogType = UnityEngine.LogType;

	public static class SystemObjectDebugExtensions
	{
		public static void Log(this object self, object message)
		{
			Debug.Logger.Log(
				LogType.Log,
				self.GetType().Name,
				message
				);
		}

		public static void Log(this object self, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Log,
				self.GetType().Name,
				MessageUtility.Join(message)
				);
		}

		public static void LogWarning(this object self, object message)
		{
			Debug.Logger.Log(
				LogType.Warning,
				self.GetType().Name,
				message
				);
		}

		public static void LogWarning(this object self, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Warning,
				self.GetType().Name,
				MessageUtility.Join(message)
				);
		}

		public static void LogError(this object self, object message)
		{
			Debug.Logger.Log(
				LogType.Error,
				self.GetType().Name,
				message
				);
		}

		public static void LogError(this object self, params object[] message)
		{
			Debug.Logger.Log(
				LogType.Error,
				self.GetType().Name,
				MessageUtility.Join(message)
				);
		}
	}
}