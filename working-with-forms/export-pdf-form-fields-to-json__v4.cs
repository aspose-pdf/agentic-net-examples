using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "template.pdf";

        // Output JSON file that will hold the exported form field appearance settings
        const string outputJsonPath = "form_fields.json";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in a using block for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export all form fields (including appearance settings) to a JSON file.
                // The ExportFieldsToJsonOptions can be customized if needed; here we use defaults.
                pdfDoc.Form.ExportToJson(outputJsonPath);
            }

            Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}