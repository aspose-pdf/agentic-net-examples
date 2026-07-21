using System;
using System.IO;
using Aspose.Pdf;

class ExportFormData
{
    // Entry point of the command‑line utility.
    static void Main(string[] args)
    {
        // Expect at least two arguments: input PDF path and output format flag.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportFormData <pdfPath> <format>");
            Console.Error.WriteLine("Supported formats: json");
            return;
        }

        string pdfPath = args[0];
        string format  = args[1].ToLowerInvariant();

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Determine output file name based on requested format.
        string outputPath = format switch
        {
            "json" => Path.ChangeExtension(pdfPath, ".json"),
            _ => null
        };

        if (outputPath == null)
        {
            Console.Error.WriteLine($"Error: Unsupported format '{format}'. Supported: json");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to JSON using the Form API.
                // The ExportToJson(string) overload writes directly to the file.
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