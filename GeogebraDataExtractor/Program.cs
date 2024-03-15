using Common;
using GeogebraDataExtractor;
using System.CommandLine;
using System.CommandLine.Invocation;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }

        string ggbfile = args[0];

        string output = null;
        // Check if the second argument is provided and is an output option
        if (args.Length > 1 && (args[1] == "-o" || args[1] == "--output"))
        {
            // Check if there's a third argument which is the output value
            if (args.Length > 2)
            {
                output = args[2];
                if (!output.Contains('.'))
                {
                    Console.WriteLine("Output is not a valid filename (missing extension)!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Output option provided but no output value specified.");
                return;
            }
        }

        var reader = new GGBReader(ggbfile);

        if(output == null)
        {
            //simple menu, with console outputs.
            return;
        }
        string fileExtension = Path.GetExtension(output)?.ToLower().TrimStart('.')!;
        Console.WriteLine($"file Extension: {fileExtension}");
        OutputType outputType;
        switch (fileExtension)
        {
            case "xlsx":
                Console.WriteLine("Excel is currently not supported");
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
        outputdata.SetPoints(reader.GetPoints());
        outputdata.CreateFile(output);
    }
}