using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output file – Aspose.Pdf provides ExportToJson for form data.
        // There is no ExportToXml method in the documented API, so we use JSON export.
        // The resulting JSON can be stored for archival and later re‑imported.
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Ensure the document actually contains a form
                if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
                {
                    Console.WriteLine("The PDF does not contain any form fields.");
                    return;
                }

                // Export form fields to JSON (the only supported export format in the API)
                using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    pdfDocument.Form.ExportToJson(jsonStream);
                }

                Console.WriteLine($"Form fields exported to JSON file: {outputJsonPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}