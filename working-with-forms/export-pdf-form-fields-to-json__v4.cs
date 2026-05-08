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

        // Output JSON file that will hold the exported form field data
        const string outputJsonPath = "formFields.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the standard Document constructor)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Prepare export options (optional). Here we request indented JSON for readability.
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true,
                    ExportPasswordValue = false // Do not export password field values
                };

                // Export all form fields to the specified JSON file.
                // The ExportToJson method writes the JSON representation of the form fields,
                // including appearance settings, to the provided file path.
                pdfDocument.Form.ExportToJson(outputJsonPath, jsonOptions);

                Console.WriteLine($"Form fields exported successfully to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}