using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "formdata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set up export options to enable pretty‑printing (indented JSON)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export all form fields to a JSON file with the specified options
            doc.Form.ExportToJson(outputJson, jsonOptions);
        }

        Console.WriteLine($"Form data exported to '{outputJson}'.");
    }
}