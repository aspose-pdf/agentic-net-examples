using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain field names and values
        const string outputJsonPath = "form_fields.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf (lifecycle rule: use using for disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all AcroForm fields to JSON.
            // The ExportToJson method writes the JSON directly to the specified file.
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"AcroForm fields have been exported to '{outputJsonPath}'.");
    }
}