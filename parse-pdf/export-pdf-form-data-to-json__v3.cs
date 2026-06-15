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
            Console.Error.WriteLine("Supported format flags: json");
            return;
        }

        string pdfPath = args[0];
        string formatFlag = args[1].ToLowerInvariant();

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Determine output file name based on the requested format
        string outputPath;
        switch (formatFlag)
        {
            case "json":
                outputPath = Path.ChangeExtension(pdfPath, ".json");
                break;
            default:
                Console.Error.WriteLine($"Error: Unsupported format '{formatFlag}'. Only 'json' is supported.");
                return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export all form fields to JSON using the built‑in ExportToJson method
                // This writes directly to the specified file; no manual Save() call is needed
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