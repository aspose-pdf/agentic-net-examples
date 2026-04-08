using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing form fields
        const string pdfPath = "input.pdf";

        // Path where the JSON representation of the form data will be saved
        const string jsonPath = "formdata.json";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileStream for writing the JSON output.
            // FileMode.Create overwrites any existing file.
            // FileAccess.Write allows writing bytes.
            // The stream will be disposed automatically by the using block.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to JSON. The ExportToJson method writes UTF‑8 encoded JSON by default.
                doc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form data exported to UTF‑8 JSON file: {jsonPath}");
    }
}
