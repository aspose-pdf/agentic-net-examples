using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_schema.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure JSON export options (optional)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // Produce readable JSON
                ExportPasswordValue = false    // Do not export password field values
            };

            // Export all form field definitions to a JSON file
            pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
        }

        Console.WriteLine($"Form field definitions exported to: {outputJsonPath}");
    }
}