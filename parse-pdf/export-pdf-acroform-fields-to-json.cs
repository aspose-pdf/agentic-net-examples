using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all AcroForm field names and values to a JSON file
            // The ExportToJson method writes the JSON representation of the form fields.
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"AcroForm fields have been exported to '{outputJsonPath}'.");
    }
}