using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set JSON export options (indent output for readability, do not export password values)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,
                ExportPasswordValue = false
            };

            // Export all form fields to a JSON file
            pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
        }

        Console.WriteLine($"Form field definitions exported to JSON at: {outputJsonPath}");
    }
}