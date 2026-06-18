using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";
        const string compressedJsonPath = "data.json.gz";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(compressedJsonPath))
        {
            Console.Error.WriteLine($"Compressed JSON not found: {compressedJsonPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Decompress the JSON payload and import form fields
            using (FileStream gzStream = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
            using (GZipStream jsonStream = new GZipStream(gzStream, CompressionMode.Decompress))
            {
                // Import form data from the JSON stream
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}