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
            // Configure JSON export options: enable pretty‑printing (indented output)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true   // true => indented (human‑readable) JSON
            };

            // Export all form fields to the specified JSON file with the options
            pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
        }

        Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
    }
}