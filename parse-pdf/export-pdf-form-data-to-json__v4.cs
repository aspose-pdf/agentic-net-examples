using System;
using System.IO;
using Aspose.Pdf;

class ExportFormData
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input PDF path and output format flag.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportFormData <input.pdf> <format> [output]");
            Console.Error.WriteLine("Supported formats: json");
            return;
        }

        string inputPath = args[0];
        string format    = args[1].ToLowerInvariant();
        string outputPath;

        // Determine output file name if not supplied.
        if (args.Length >= 3)
        {
            outputPath = args[2];
        }
        else
        {
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            switch (format)
            {
                case "json":
                    outputPath = $"{baseName}_form.json";
                    break;
                default:
                    Console.Error.WriteLine($"Unsupported format '{format}'.");
                    return;
            }
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the standard Aspose.Pdf constructor.
            using (Document doc = new Document(inputPath))
            {
                // Export form fields according to the requested format.
                switch (format)
                {
                    case "json":
                        // Export all form fields to a JSON file.
                        // The ExportToJson overload writes directly to the specified file.
                        doc.Form.ExportToJson(outputPath);
                        Console.WriteLine($"Form data exported to JSON: {outputPath}");
                        break;

                    default:
                        Console.Error.WriteLine($"Format '{format}' is not implemented.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}