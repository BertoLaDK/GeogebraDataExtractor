using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.XmlClasses
{
    using System.Xml.Serialization;

    [XmlRoot("geogebra",Namespace = "")]
    public class GGBRoot
    {
        [XmlElement("construction")]
        public GGBConstruction GGBConstruction { get; set; }
    }
    public class GGBConstruction
    {
        [XmlElement("element")]
        public List<ConstructionElement> Elements { get; set; }
    }

    public class ConstructionElement
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("label")]
        public string Label { get; set; }

        [XmlElement("coords")]
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        [XmlAttribute("x")]
        public double X { get; set; }

        [XmlAttribute("y")]
        public double Y { get; set; }

        [XmlAttribute("z")]
        public double Z { get; set; }
    }
}
