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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options (optional)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // Produce readable, indented JSON
                ExportPasswordValue = false    // Do not include password values in the output
            };

            // Export all form field definitions to a JSON file
            doc.Form.ExportToJson(outputJson, jsonOptions);
        }

        Console.WriteLine($"Form definitions exported to '{outputJson}'.");
    }
}