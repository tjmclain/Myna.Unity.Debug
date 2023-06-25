using System;
using System.Collections.Generic;
using System.Text;

namespace Myna.Stats
{
	[Serializable]
	public class Stat
	{
		protected string _id = "";
		protected float _baseValue;

		[NonSerialized] protected float _value;

		public virtual string Id => _id;
		public virtual float BaseValue => _baseValue;
		public virtual float Value => _value;

		public virtual void Initialize()
		{
			_value = _baseValue;
		}
	}
}
