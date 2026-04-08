using System;
using System.IO;
using Aspose.Pdf;

class ExportFormData
{
    // Entry point of the command‑line utility
    static void Main(string[] args)
    {
        // Expect exactly two arguments: input PDF path and output format flag
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: ExportFormData <pdfPath> <format>");
            Console.Error.WriteLine("Supported formats: json");
            return;
        }

        string pdfPath = args[0];
        string format  = args[1].ToLowerInvariant();

        // Verify that the input PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'");
            return;
        }

        // Determine output file name based on the requested format
        string outputPath;
        switch (format)
        {
            case "json":
                outputPath = Path.ChangeExtension(pdfPath, ".json");
                break;
            default:
                Console.Error.WriteLine($"Error: Unsupported format '{format}'. Only 'json' is supported.");
                return;
        }

        try
        {
            // Load the PDF document inside a using block (ensures deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to JSON using the built‑in ExportToJson method.
                // This writes directly to the specified file; no additional save logic is required.
                doc.Form.ExportToJson(outputPath);
            }

            Console.WriteLine($"Form data exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or exporting
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}