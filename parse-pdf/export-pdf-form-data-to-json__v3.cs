using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "formdata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream to write the JSON output
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                // Optional export options
                ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true,
                    ExportPasswordValue = false
                };

                // Export all form fields to the JSON stream
                doc.Form.ExportToJson(jsonStream, options);
            }
        }

        Console.WriteLine($"Form data exported to '{outputJson}'.");
    }
}