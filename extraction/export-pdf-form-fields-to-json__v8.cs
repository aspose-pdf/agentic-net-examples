using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormA.pdf";
        const string outputJson = "FormA_fields.json"; // Core API exports to JSON, not FDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enumerate all form fields and output their names and values
            foreach (var field in doc.Form.Fields)
            {
                Console.WriteLine($"Field: {field.FullName}, Value: {field.Value}");
            }

            // Export the complete form data to a JSON file (core API does not provide FDF export)
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream);
            }
        }

        Console.WriteLine($"Form data exported to '{outputJson}'.");
    }
}
