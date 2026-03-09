using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_fields.json";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create (or overwrite) the JSON output file
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to JSON.
                // The method returns a collection of results which we ignore here.
                doc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form fields have been exported to '{outputJson}'.");
    }
}