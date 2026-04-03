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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Set pretty‑printing (indented) option for JSON output
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true
                };

                // Export all form fields to a JSON file
                doc.Form.ExportToJson(outputJson, jsonOptions);
            }

            Console.WriteLine($"Form data successfully exported to '{outputJson}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}