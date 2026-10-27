using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Supported export format – the core API provides JSON export for form data.
    enum ExportFormat
    {
        JSON
    }

    static void Main(string[] args)
    {
        // Expect: <pdfPath>
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: ExportFormData <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // The only supported export format with the core API is JSON.
        ExportFormat exportFormat = ExportFormat.JSON;
        string outputPath = Path.ChangeExtension(pdfPath, "json");

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document actually contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Export form data to JSON – this is the supported core‑API method.
                using (FileStream jsonStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    doc.Form.ExportToJson(jsonStream);
                }
            }

            Console.WriteLine($"Form data exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
