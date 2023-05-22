using System;
using System.Collections.Generic;

namespace Myna.Stats
{
	public class StatsController
	{
		public List<Stat> Stats = new List<Stat>();
		public List<Attribute> Attributes = new List<Attribute>();

		protected readonly Dictionary<string, Stat> _stats = new Dictionary<string, Stat>();
		protected readonly Dictionary<string, Attribute> _attributes = new Dictionary<string, Attribute>();

		public virtual void Initialize()
		{
			_stats.Clear();
			_attributes.Clear();

			foreach (var stat in Stats)
			{
				stat.Initialize();
				_stats[stat.Id] = stat;
			}

			foreach (var attribute in Attributes)
			{
				float minValue = ResolveValue(attribute.Min);
				float maxValue = ResolveValue(attribute.Max);
				attribute.Initialize(minValue, maxValue);

				_attributes[attribute.Id] = attribute;
			}
		}

		public virtual float GetValue(string id)
		{
			return TryGetValue(id, out var value) ? value : 0f;
		}

		protected virtual float ResolveValue(string value)
		{
			if (float.TryParse(value, out float result))
			{
				return result;
			}

			if (TryGetValue(value, out result))
			{
				return result;
			}

			Console.Error.WriteLine($"failed to resolve value '{value}'");
			return 0f;
		}

		protected virtual bool TryGetValue(string id, out float result)
		{
			if (_stats.TryGetValue(id, out var stat))
			{
				result = stat.Value;
				return true;
			}

			if (_attributes.TryGetValue(id, out var attribute))
			{
				result = attribute.Value;
				return true;
			}

			Console.Error.WriteLine($"found no stat with id '{id}'");
			result = 0f;
			return false;
		}
	}
}
