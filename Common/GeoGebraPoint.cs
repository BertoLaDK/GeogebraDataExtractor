using Common.XmlClasses;

namespace Common
{
	public class GeoGebraPoint
	{
		public GeoGebraPoint(ConstructionElement element)
		{
			Name = element.Label;
			X = element.Coordinates.X;
			Y = element.Coordinates.Y;

		}

		public string Name { get; set; }
		public double X { get; set; }
		public double Y { get; set; }

		public string ToString(bool withoutName = false)
		{
			if (withoutName)
			{
				return $"({X};{Y})";
			}
			else
			{
				return $"{Name}: ({X};{Y})";
			}
		}
	}
}
