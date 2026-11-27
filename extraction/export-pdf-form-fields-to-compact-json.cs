using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                // Configure export options: compact JSON (no indentation).
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = false
                };

                // Export all form fields to the specified JSON file using the Form API.
                // The ExportToJson overload accepts a stream and the options object.
                doc.Form.ExportToJson(jsonStream, jsonOptions);
            }

            Console.WriteLine($"Form fields exported to '{outputJson}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
