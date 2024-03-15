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

		public override string ToString()
		{
			return $"{Name}: ({X};{Y})";
		}
    }
}
