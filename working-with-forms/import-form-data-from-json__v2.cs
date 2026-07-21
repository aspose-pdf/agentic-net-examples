using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formData.json";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Bulk import form field values from the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}