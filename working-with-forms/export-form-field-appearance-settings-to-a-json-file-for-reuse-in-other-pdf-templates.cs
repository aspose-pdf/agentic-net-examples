using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputJson = "formAppearance.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options (optional)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                ExportPasswordValue = false, // do not export password values
                WriteIndented = true         // produce readable, indented JSON
            };

            // Export all form fields (including appearance settings) to a JSON file
            // Form.ExportToJson(string, ExportFieldsToJsonOptions) writes directly to the file
            doc.Form.ExportToJson(outputJson, jsonOptions);
        }

        Console.WriteLine($"Form fields exported to '{outputJson}'.");
    }
}