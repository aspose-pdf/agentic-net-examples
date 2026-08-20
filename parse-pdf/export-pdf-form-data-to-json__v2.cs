using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formdata.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Export all form fields to a UTF‑8 encoded JSON file
                using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    // Correct API: ExportToJson writes the form data as UTF‑8 JSON
                    doc.Form.ExportToJson(fs);
                }
            }

            Console.WriteLine($"Form data exported to '{jsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
