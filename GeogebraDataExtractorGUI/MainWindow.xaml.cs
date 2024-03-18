using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using System.DirectoryServices.ActiveDirectory;

namespace GeogebraDataExtractorGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<GeoGebraPoint> Points { get; set; }
		public string Format { get; set; }
		public MainWindow()
		{
			InitializeComponent();
		}
		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Geogebra files |*.ggb";
			if (openFileDialog.ShowDialog() == true)
			{
				ggbFileName.Text = openFileDialog.FileName;
				var reader = new GGBReader(openFileDialog.FileName);
				Points = reader.GetPoints();
				if (Format != null)
				{
					var outputdata = new Output(OutputType.Txt);
					outputdata.Format = Format;
					outputdata.SetPoints(Points);
					previewTextBox.Text = outputdata.ToString();
				}
				else
				{
					previewTextBox.Text = string.Join("\n", Points.Select(x => x.ToString()));
				}
			}
		}

		private void btnSaveFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Text file|*.txt";
			var outputdata = new Output(OutputType.Txt);
			outputdata.Format = Format;
			outputdata.SetPoints(Points);
			if (saveFileDialog.ShowDialog() == true)
			{
				using (StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile()))
				{
					sw.WriteLine(outputdata.ToString());
				}
			}
		}

		private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
			if (selectedItem != null)
			{
				string selectedValue = selectedItem.Content.ToString();

				var outputdata = new Output(OutputType.Txt);
				if (selectedValue != null)
				{
					Format = selectedValue;
					outputdata.Format = selectedValue;
				}
				outputdata.SetPoints(Points);
				previewTextBox.Text = outputdata.ToString();
			}
		}
	}
}