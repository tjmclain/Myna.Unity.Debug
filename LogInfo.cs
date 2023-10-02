using System;
using System.Collections.Generic;
using System.Text;

namespace Myna.Unity.Debug
{
	using UnityObject = UnityEngine.Object;

	public class LogInfo
	{
		private readonly List<object> _message = new List<object>(4);
		private string _tag = string.Empty;
		private UnityObject _context = null;
		private bool _isValid = false;

		public LogInfo Append(object message)
		{
			if (_isValid)
			{
				_message.Add(message);
			}

			return this;
		}
	}
}