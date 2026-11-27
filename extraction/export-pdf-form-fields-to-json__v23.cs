using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Configure export options: pretty‑print JSON and include password values
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,
                ExportPasswordValue = true
            };

            // Export all form fields to JSON using the correct API overload
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream, jsonOptions);
            }
        }

        Console.WriteLine($"Form fields have been exported to '{outputJson}'.");
    }
}
