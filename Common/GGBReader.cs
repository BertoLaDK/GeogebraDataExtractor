using Common;
using Common.XmlClasses;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public class GGBReader
    {
        public GGBRoot XmlRoot { get; set; }
        public GGBReader(string filename)
        {
            using (ZipArchive archive = ZipFile.OpenRead(filename))
            {
                ZipArchiveEntry entry = archive.GetEntry("geogebra.xml");
                if (entry != null)
                {
                    using (Stream entryStream = entry.Open())
                    using (StreamReader reader = new StreamReader(entryStream, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(GGBRoot));
                        XmlRoot = (GGBRoot)serializer.Deserialize(reader);
                    }
                    Console.WriteLine("Extraction complete.");
                }
                else
                {
                    Console.WriteLine("File not found in archive.");

                }
            }
            

        }

        public List<GeoGebraPoint> GetPoints()
        {
            List<GeoGebraPoint> geoGebraPoints = new List<GeoGebraPoint>();

            foreach (var element in XmlRoot.GGBConstruction.Elements.Where(x=>x.Type == "point"))
            {
                geoGebraPoints.Add(new GeoGebraPoint(element));
            }

            return geoGebraPoints;
        }

    }
}
