using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

	public enum OutputType
	{
		Txt,
		Csv,
		Excel
	}

	public class Output
	{
		public OutputType Type { get; set; }
		public Output(OutputType type) { Type = type; }
        private List<GeoGebraPoint> Points { set; get; }
        public void CreateFile(string filename)
		{
			switch (Type)
			{
				case OutputType.Txt:
					CreateTxtOutput(filename);
					break;
				case OutputType.Csv:
					CreateCsvOutput(filename);
					break;
			}
			return;
		}


		void CreateTxtOutput(string filename)
		{
			using (var sw = new StreamWriter(filename)) {
				foreach (var point in Points)
				{
					sw.WriteLine(point.ToString());
				}
			}
		}

        void CreateCsvOutput(string filename)
        {
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Points);
            }
        }

        public Output SetPoints(List<GeoGebraPoint> points)
		{
			Points = points;
			return this;
		}
	}
}
