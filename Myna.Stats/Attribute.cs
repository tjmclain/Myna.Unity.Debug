using System;

namespace Myna.Stats
{
	[Serializable]
	public class Attribute : Stat
	{
		protected string _min = "";
		protected string _max = "";

		[NonSerialized] protected float _minValue;
		[NonSerialized] protected float _maxValue;

		public virtual string Min => _min;
		public virtual string Max => _max;
		public virtual float MinValue => _minValue;
		public virtual float MaxValue => _maxValue;

		public virtual void Initialize(float minValue, float maxValue)
		{
			_minValue = minValue;
			_maxValue = maxValue;
			_value = MathUtility.Lerp(minValue, maxValue, _baseValue);
		}
	}
}
