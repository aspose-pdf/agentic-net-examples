using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Access to Form and ExportOptions

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "template.pdf";

        // Output JSON file that will hold the exported form field definitions
        const string outputJsonPath = "form_fields.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Optional: configure export options (e.g., include password values, indent JSON)
                ExportFieldsToJsonOptions exportOptions = new ExportFieldsToJsonOptions
                {
                    ExportPasswordValue = true,   // include password field values if any
                    WriteIndented = true          // produce pretty‑printed JSON
                };

                // Export all form fields to the specified JSON file
                // The Form.ExportToJson method writes the JSON representation of the form
                // (including appearance settings) to the given file.
                pdfDoc.Form.ExportToJson(outputJsonPath, exportOptions);

                Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}