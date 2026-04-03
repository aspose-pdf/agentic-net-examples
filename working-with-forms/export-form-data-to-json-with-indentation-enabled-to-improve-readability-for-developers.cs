using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormDataToJson
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file path
        const string outputJsonPath = "formData.json";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare export options with indentation enabled
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true   // Enable pretty‑printed JSON
            };

            // Export all form fields to the specified JSON file
            pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
        }

        Console.WriteLine($"Form data exported to '{outputJsonPath}' with indentation.");
    }
}