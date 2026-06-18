using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Form handling (Form, Field, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath   = "input.pdf";      // PDF with form fields
        const string jsonPath  = "data.json";      // JSON array with field values
        const string outputPdf = "filled_output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Bulk import of form field values from the JSON file.
                // ImportFromJson reads the JSON and updates matching fields.
                doc.Form.ImportFromJson(jsonPath);

                // Save the updated document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}