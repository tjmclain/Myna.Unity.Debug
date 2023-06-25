using System;

namespace Myna.Utilities
{
	public static class MathUtility
	{
		public static int RoundToInt(double value)
		{
			value = Math.Round(value);
			return Convert.ToInt32(value);
		}

		public static int RoundToInt(float value)
		{
			double d = Convert.ToDouble(value);
			return RoundToInt(d);
		}

		public static float Round(float value, int digits = 0)
		{
			double d = Convert.ToDouble(value);
			d = Math.Round(d, digits);
			return Convert.ToSingle(d);
		}

		public static int CeilToInt(double value)
		{
			value = Math.Ceiling(value);
			return Convert.ToInt32(value);
		}

		public static int CeilToInt(float value)
		{
			double d = Convert.ToDouble(value);
			return CeilToInt(d);
		}

		public static int FloorToInt(double value)
		{
			value = Math.Floor(value);
			return Convert.ToInt32(value);
		}

		public static int FloorToInt(float value)
		{
			double d = Convert.ToDouble(value);
			return FloorToInt(d);
		}

		public static double Lerp(double a, double b, double t)
		{
			return a + (b - a) * t;
		}

		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * t;
		}
	}
}
