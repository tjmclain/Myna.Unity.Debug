using System;
using System.Diagnostics;
using Myna.Unity.Debug;
using LogType = UnityEngine.LogType;

// static wrapper for Logger calls
public static class MynaDebug
{
	public static readonly Logger Logger = new Logger();

	[Conditional("DEBUG")]
	public static void Log(string message) =>
		Logger.Log(LogType.Log, message);

	[Conditional("DEBUG")]
	public static void LogWarning(string message) =>
		Logger.Log(LogType.Warning, message);

	[Conditional("DEBUG")]
	public static void LogError(string message) =>
		Logger.Log(LogType.Error, message);

	public static void LogException(Exception exception) =>
		UnityEngine.Debug.LogException(exception);

}
