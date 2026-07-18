using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will hold the exported schema
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (no special load options required for standard PDFs)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare export options – indent the JSON for readability
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true,
                    ExportPasswordValue = false // do not expose password field values
                };

                // Export all form fields to the specified JSON file
                pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);

                Console.WriteLine($"Form field definitions exported successfully to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}