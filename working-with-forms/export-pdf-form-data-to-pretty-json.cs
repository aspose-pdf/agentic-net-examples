using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Form handling

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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure JSON export options for pretty‑printing
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true   // Enable indentation for readability
                };

                // Export all form fields to the specified JSON file
                pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
            }

            Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}