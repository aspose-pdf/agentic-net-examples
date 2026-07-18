using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formdata.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Export all form fields to a JSON file.
                // The ExportToJson(string) overload writes the JSON directly to the specified file.
                // Aspose.Pdf writes UTF‑8 encoded JSON by default.
                doc.Form.ExportToJson(jsonPath);
            }

            Console.WriteLine($"Form data successfully exported to '{jsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}