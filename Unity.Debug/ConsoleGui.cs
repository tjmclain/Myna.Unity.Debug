using UnityEngine;

namespace Myna.Unity.Debug
{
	public class ConsoleGUI
	{
		private Vector2 _scrollPos = new();
		private string _filePath = "";
		private string _filter = "";

		public void Show()
		{
			static Color GetLogColor(LogType logType)
			{
				return logType switch
				{
					LogType.Warning => Color.yellow,
					LogType.Error or LogType.Exception or LogType.Assert => Color.red,
					_ => Color.white,
				};
			}

			// filter field
			GUILayout.BeginHorizontal();
			GUILayout.Label("Filter", GUILayout.ExpandWidth(false));
			_filter = GUILayout.TextField(_filter, GUILayout.ExpandWidth(true));
			GUILayout.EndHorizontal();

			// logs scrollview
			_scrollPos = GUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));

			var logs = Console.GetLogs()
				.Where(x => string.IsNullOrEmpty(_filter) || x.Message.Contains(_filter))
				.Reverse();

			var originalColor = GUI.color;
			foreach (var log in logs)
			{
				GUI.color = GetLogColor(log.LogType);
				GUILayout.Label(log.Message);
			}
			GUI.color = originalColor;

			GUILayout.EndScrollView();

			// write to file path
			GUILayout.BeginHorizontal();
			GUILayout.Label("File Path", GUILayout.ExpandWidth(false));
			_filePath = GUILayout.TextField(_filePath, GUILayout.ExpandWidth(true));

			bool wasEnabled = GUI.enabled;
			GUI.enabled = Uri.IsWellFormedUriString(_filePath, UriKind.RelativeOrAbsolute);
			if (GUILayout.Button("Export", GUILayout.ExpandWidth(false)))
			{
				Console.WriteLogsToFile(_filePath);
			}
			GUI.enabled = wasEnabled;
			GUILayout.EndHorizontal();
		}
	}
}
