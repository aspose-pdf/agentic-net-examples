using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all form fields to a JSON file (method provided by Aspose.Pdf.Forms.Form)
            // The method returns a collection of FieldSerializationResult which can be ignored here
            pdfDoc.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"Form data exported to '{outputJsonPath}'.");
    }
}