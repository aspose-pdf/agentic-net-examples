using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string compressedJsonPath = "data.json.gz";
        const string outputPath = "output.pdf";

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
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Open the compressed JSON file and decompress it on the fly
                using (FileStream fs = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
                using (GZipStream gz = new GZipStream(fs, CompressionMode.Decompress))
                {
                    // Import form fields from the decompressed JSON stream
                    doc.Form.ImportFromJson(gz);
                }

                // Save the updated PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}