using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // Source PDF with form fields
        const string compressedJsonPath = "data.json.gz"; // GZIP‑compressed JSON payload
        const string outputPdfPath = "output.pdf";        // Resulting PDF after import

        // Verify input files exist
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
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(pdfPath))
            {
                // Open the compressed JSON file and decompress on the fly
                using (FileStream fileStream = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    // Import form fields from the JSON stream (Form.ImportFromJson)
                    // The method returns a collection of results which we ignore here
                    doc.Form.ImportFromJson(gzipStream);
                }

                // Save the updated PDF (lifecycle rule: save inside using)
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