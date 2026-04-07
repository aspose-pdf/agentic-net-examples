using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "formdata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Set up export options with indentation enabled
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export all form fields to a JSON file
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream, options);
            }
        }

        Console.WriteLine($"Form data exported to '{outputJson}' with indentation.");
    }
}