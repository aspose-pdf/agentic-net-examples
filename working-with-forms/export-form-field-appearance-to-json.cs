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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Optional: configure JSON export options
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                ExportPasswordValue = false, // do not export password values
                WriteIndented = true         // produce indented (readable) JSON
            };

            // Export all form fields (including appearance settings) to a JSON file
            doc.Form.ExportToJson(outputJson, options);
        }

        Console.WriteLine($"Form fields exported to '{outputJson}'.");
    }
}