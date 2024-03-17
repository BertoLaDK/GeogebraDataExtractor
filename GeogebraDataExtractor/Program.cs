using Common;
using GeogebraDataExtractor;
using System.CommandLine;
using System.CommandLine.Invocation;

public class Program
{
    public static int Main(string[] args)
    {
        var ggbfileArgument = new Argument<FileInfo>() { 
            Name = "ggbfile",
            Description = "The ggb file to extract data from",
            Arity = ArgumentArity.ExactlyOne
        };

        var outputOption = new Option<FileInfo?>(
			new[]{ "--output","-o" },
            description: "the file to output it to"
        );

		var formatOption = new Option<string?>(
			new[] { "--format", "-f" },
			description: "format specifier, use one of preset formats for " +
			"txt: [xyarrays, wolabel, maple]"
			);


        var rootCommand = new RootCommand("A simple commandline solution for extracting data from ggb files");
        rootCommand.AddArgument(ggbfileArgument);
        rootCommand.AddOption(outputOption);
		rootCommand.AddOption(formatOption);

        rootCommand.SetHandler((ggbfile, output, format) =>
        {
			RunProgram(ggbfile, output, format);
		}, ggbfileArgument, outputOption, formatOption);

        return rootCommand.Invoke(args);
    }


    static void RunProgram(FileInfo ggbfile, FileInfo output, string format)
    {
		var reader = new GGBReader(ggbfile.FullName);

		if (output == null)
		{
			//simple menu, with console outputs.
			return;
		}
		string fileExtension = output.Extension.TrimStart('.');
		OutputType outputType;
		switch (fileExtension)
		{
			case "xlsx":
				Console.WriteLine("Excel is not supported, use csv instead and import it.");
				return;
			//outputType = OutputType.Excel;
			//break;
			case "csv":
				outputType = OutputType.Csv;
				break;
			case "txt":
				outputType = OutputType.Txt;
				break;

			default:
				Console.WriteLine("Unsupported Output Type");
				return;
		}

		var outputdata = new Output(outputType);
		if(format != null)
			outputdata.Format = format;
		outputdata.SetPoints(reader.GetPoints());
		outputdata.CreateFile(output.FullName);
	}
}