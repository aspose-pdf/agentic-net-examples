using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormData
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output format flag (e.g., "json")
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportFormData <input-pdf> <format>");
            Console.Error.WriteLine("Supported formats: json");
            return;
        }

        string inputPath = args[0];
        string format = args[1].ToLowerInvariant();

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        // Determine output file name based on requested format
        string outputPath;
        switch (format)
        {
            case "json":
                outputPath = Path.ChangeExtension(inputPath, ".json");
                break;
            default:
                Console.Error.WriteLine($"Error: Unsupported format '{format}'. Only 'json' is supported.");
                return;
        }

        try
        {
            // Load the PDF document (lifecycle: creation and loading)
            using (Document doc = new Document(inputPath))
            {
                // Export form fields to JSON (writes directly to the file)
                doc.Form.ExportToJson(outputPath);
            }

            Console.WriteLine($"Form data exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}