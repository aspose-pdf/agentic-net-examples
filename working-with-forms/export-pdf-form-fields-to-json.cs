using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_schema.json";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options (indentation for readability)
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,
                ExportPasswordValue = false // do not export password field values
            };

            // Export all form fields to a JSON file (Form.ExportToJson overload)
            doc.Form.ExportToJson(outputJson, options);
        }

        Console.WriteLine($"Form fields exported to JSON: {outputJson}");
    }
}