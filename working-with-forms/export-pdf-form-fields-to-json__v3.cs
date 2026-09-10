using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_schema.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure JSON export options
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // Produce readable JSON
                ExportPasswordValue = false    // Do not export password field values
            };

            // Export all form fields to a JSON file
            pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
        }

        Console.WriteLine($"Form fields exported to JSON: {outputJsonPath}");
    }
}