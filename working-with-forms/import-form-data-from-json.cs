using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for input PDF, JSON data file, and output PDF
        const string inputPdfPath  = "input.pdf";
        const string jsonDataPath  = "formData.json";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load the PDF document and import the form data in bulk
        using (Document doc = new Document(inputPdfPath))
        {
            // ImportJson expects a stream; the JSON should be an array of field objects
            using (FileStream jsonStream = File.OpenRead(jsonDataPath))
            {
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
