using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // for ExportFieldsToJsonOptions if needed

class ExportFormDataToJson
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "formData.json";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the output FileStream inside a using block
            using (FileStream jsonStream = new FileStream(
                       outputJsonPath,
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None))
            {
                // Export all form fields to JSON and write directly to the stream.
                // No options are required; you can pass an ExportFieldsToJsonOptions instance
                // if you need to customize the output (e.g., indentation, password export).
                pdfDoc.Form.ExportToJson(jsonStream);
            }

            // No additional save is required because ExportToJson writes to the provided stream.
        }

        Console.WriteLine($"Form data exported to JSON file: {outputJsonPath}");
    }
}