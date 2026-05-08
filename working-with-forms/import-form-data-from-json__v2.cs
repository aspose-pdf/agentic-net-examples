using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the JSON data file and the output PDF
        const string pdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "output.pdf";

        // Verify that the required files exist
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

        // Load the PDF document, import form data from JSON, and save the result
        using (Document doc = new Document(pdfPath))
        {
            // Bulk import all form fields from the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Persist the changes to a new PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPath}'.");
    }
}