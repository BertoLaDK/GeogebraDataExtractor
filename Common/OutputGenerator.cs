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
			Points = new List<GeoGebraPoint>();
		}

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

			using (var sw = new StreamWriter(filename))
			{
				sw.Write(this.ToString());
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

		public override string ToString()
		{
			string outputstring = string.Empty;
			if (Points == null || Points.Count == 0)
				return "No Points";
			using (StringWriter sw = new StringWriter())
			{

				if (Format == "default")
				{
					foreach (var point in Points)
					{
						sw.WriteLine(point.ToString());
					}
				}
				else if (Format == "xyarrays" || Format == "maple")
				{
					List<double> xValues = Points.Select(obj => obj.X).ToList();
					List<double> yValues = Points.Select(obj => obj.Y).ToList();
					if (Format == "maple")
						DecimalSeparator = '.';

					char arraySeparator = DecimalSeparator == ',' ? ';' : ',';
					NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
					numberFormatInfo.NumberDecimalDigits = 8;
					numberFormatInfo.NumberDecimalSeparator = DecimalSeparator.ToString();
					string xValuesString;
					string yValuesString;
					if (Format == "maple")
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
				else if (Format == "wolabel")
				{
					foreach (var point in Points)
					{
						sw.WriteLine(point.ToString(true));
					}
				}
				outputstring = sw.ToString();
			}
			return outputstring;
		}

		public Output SetPoints(List<GeoGebraPoint> points)
		{
			Points = points;
			return this;
		}
	}
}
