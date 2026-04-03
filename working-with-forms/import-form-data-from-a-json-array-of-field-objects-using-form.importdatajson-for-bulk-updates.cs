using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Bulk import form fields from the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}