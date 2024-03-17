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
		public string Format { get; set; }
		public char DecimalSeparator { get; set; }
		public Output(OutputType type)
		{
			Type = type;
			Format = "default";
			DecimalSeparator = ',';
		}

		private List<GeoGebraPoint> Points { set; get; }
		public void CreateFile(string filename)
		{
			switch (Type)
			{
				case OutputType.Txt:
					CreateTxtOutput(filename, Format);
					break;
				case OutputType.Csv:
					CreateCsvOutput(filename);
					break;
			}
			return;
		}


		void CreateTxtOutput(string filename, string format)
		{

			using (var sw = new StreamWriter(filename))
			{
				if (format == "default")
				{
					foreach (var point in Points)
					{
						sw.WriteLine(point.ToString());
					}
				}
				else if (format == "xyarrays" || format == "maple")
				{
					List<double> xValues = Points.Select(obj => obj.X).ToList();
					List<double> yValues = Points.Select(obj => obj.Y).ToList();
					if (format == "maple")
						DecimalSeparator = '.';

					char arraySeparator = DecimalSeparator == ',' ? ';' : ',';
					NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
					numberFormatInfo.NumberDecimalSeparator = DecimalSeparator.ToString();
					string xValuesString;
					string yValuesString;
					if (format == "maple")
					{
						xValuesString = "X := [" + string.Join($"{arraySeparator} ", xValues.Select(x => x.ToString(numberFormatInfo))) + "]";
						yValuesString = "Y := [" + string.Join($"{arraySeparator} ", yValues.Select(x => x.ToString(numberFormatInfo))) + "]";
					}
					else
					{
						xValuesString = "X: [" + string.Join($"{arraySeparator} ", xValues.Select(x => x.ToString(numberFormatInfo))) + "]";
						yValuesString = "Y: [" + string.Join($"{arraySeparator} ", yValues.Select(x => x.ToString(numberFormatInfo))) + "]";
					}
					Console.WriteLine(xValuesString);
					Console.WriteLine(yValuesString);
					sw.WriteLine(xValuesString);
					sw.WriteLine(yValuesString);
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
