using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Export all AcroForm fields (names and values) to a JSON file
            // ExportToJson writes the JSON representation directly to the specified file
            doc.Form.ExportToJson(outputJson);
        }

        Console.WriteLine($"AcroForm fields have been exported to '{outputJson}'.");
    }
}