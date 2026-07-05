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

        // Output JSON file that will hold the form field schema
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure export options (optional)
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    // Include password values if the form contains password fields
                    ExportPasswordValue = true,
                    // Produce indented (pretty‑printed) JSON for readability
                    WriteIndented = true
                };

                // Export all form fields to the specified JSON file
                pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);
            }

            Console.WriteLine($"Form field definitions exported to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}