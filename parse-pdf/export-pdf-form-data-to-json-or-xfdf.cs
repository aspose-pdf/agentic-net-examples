using System;
using System.IO;
using Aspose.Pdf;

class FormExportUtility
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: PDF path and output format flag.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: FormExportUtility <pdfPath> <format>");
            Console.Error.WriteLine("Supported formats: json, xfdf");
            return;
        }

        string pdfPath = args[0];
        string format = args[1].ToLowerInvariant();

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document using the standard Aspose.Pdf constructor.
            using (Document doc = new Document(pdfPath))
            {
                switch (format)
                {
                    case "json":
                        // Export form fields to a JSON file. The method writes directly to the file.
                        string jsonPath = Path.ChangeExtension(pdfPath, ".json");
                        doc.Form.ExportToJson(jsonPath);
                        Console.WriteLine($"Form data exported to JSON: {jsonPath}");
                        break;

                    case "xfdf":
                        // Export all annotations (including form fields) to an XFDF file.
                        string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                        doc.ExportAnnotationsToXfdf(xfdfPath);
                        Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");
                        break;

                    default:
                        Console.Error.WriteLine($"Error: Unsupported format '{format}'. Supported formats are json and xfdf.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}