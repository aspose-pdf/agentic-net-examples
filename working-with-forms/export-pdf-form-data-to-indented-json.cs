using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formdata.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Configure export options to enable indentation
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export all form fields to a JSON file with indentation
            using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(fs, jsonOptions);
            }

            Console.WriteLine($"Form data exported to '{jsonPath}' with indentation.");
        }
    }
}