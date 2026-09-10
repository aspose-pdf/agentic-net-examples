using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string compressedJsonPath = "data.json.gz";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(compressedJsonPath))
        {
            Console.Error.WriteLine($"Compressed JSON not found: {compressedJsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Open the compressed JSON file and decompress it on the fly
                using (FileStream fileStream = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    // Import form fields from the JSON stream
                    doc.Form.ImportFromJson(gzipStream);
                }

                // Save the updated PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}