using System;
using System.Collections.Generic;
using System.Text;

namespace Myna.Stats
{
	public static class MathUtility
	{
		public static float Lerp(float a, float b, float t)
		{
			return a * (1f - t) + (b * t);
		}
	}
}
