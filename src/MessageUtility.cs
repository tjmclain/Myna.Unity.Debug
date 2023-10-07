using System.Collections.Generic;

namespace Myna.Unity.Debug
{
	public static class MessageUtility
	{
		private const string _delimeter = " ";

		public static string Join(IEnumerable<object> message)
		{
			return string.Join(_delimeter, message);
		}
	}
}