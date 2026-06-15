using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Forms;               // Access to Form class

class ExportFormFieldsToJson
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will hold the form schema
        const string outputJsonPath = "form_schema.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the recommended lifecycle rule)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare export options – enable pretty‑printed (indented) JSON
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true,
                    ExportPasswordValue = false   // Do not export password field values
                };

                // Export the entire form definition to a JSON file
                // The Form.ExportToJson method writes the JSON representation of all fields
                pdfDoc.Form.ExportToJson(outputJsonPath, jsonOptions);

                Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., corrupted PDF, I/O issues)
            Console.Error.WriteLine($"Export failed: {ex.Message}");
        }
    }
}
