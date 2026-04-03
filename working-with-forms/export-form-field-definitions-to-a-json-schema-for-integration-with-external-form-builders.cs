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
        const string outputJsonPath = "formFields.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare export options (optional). Here we request indented JSON for readability.
            ExportFieldsToJsonOptions exportOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,
                ExportPasswordValue = false // Do not export password field values
            };

            // Export all form fields to the specified JSON file.
            // The Form property gives access to the collection of form fields.
            pdfDoc.Form.ExportToJson(outputJsonPath, exportOptions);
        }

        Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
    }
}