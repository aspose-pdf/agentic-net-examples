using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // For ExportFieldsToJsonOptions (if needed)

class ExportFormDataToJson
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formData.json";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for the JSON output
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Optional: configure export options (e.g., indented JSON)
                ExportFieldsToJsonOptions exportOptions = new ExportFieldsToJsonOptions {
                    WriteIndented = true,
                    ExportPasswordValue = false // example setting; adjust as needed
                };

                // Export all form fields to the JSON stream
                pdfDoc.Form.ExportToJson(jsonStream, exportOptions);
            }
        }

        Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
    }
}