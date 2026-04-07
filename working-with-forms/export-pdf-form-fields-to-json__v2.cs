using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "form_schema.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Configure JSON export options
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // make JSON human‑readable
                ExportPasswordValue = false    // do not include password values
            };

            // Export all form fields to a JSON file
            doc.Form.ExportToJson(jsonPath, options);
        }

        Console.WriteLine($"Form field schema exported to '{jsonPath}'.");
    }
}