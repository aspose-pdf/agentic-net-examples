using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_schema.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // pretty‑print JSON
                ExportPasswordValue = false    // do not include password values
            };

            // Export all form fields to a JSON file
            doc.Form.ExportToJson(outputJson, options);
        }

        Console.WriteLine($"Form field definitions exported to '{outputJson}'.");
    }
}