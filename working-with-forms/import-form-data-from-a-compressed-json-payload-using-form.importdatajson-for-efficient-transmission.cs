using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the compressed JSON payload, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string compressedJsonPath = "formdata.json.gz";
        const string outputPdfPath = "filled.pdf";

        // Verify that the input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(compressedJsonPath))
        {
            Console.Error.WriteLine($"Compressed JSON not found: {compressedJsonPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Open the compressed JSON file and decompress it on the fly
            using (FileStream fileStream = new FileStream(compressedJsonPath, FileMode.Open, FileAccess.Read))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                // Import form fields from the decompressed JSON stream.
                // The method returns a collection of FieldSerializationResult objects.
                // In current Aspose.Pdf versions these objects do not expose IsSuccessful, FieldName or ErrorMessage properties.
                // If you need diagnostic information, you can inspect the result via its ToString() method or simply ignore it.
                var importResults = pdfDoc.Form.ImportFromJson(gzipStream);
                // Optional: log each result using ToString()
                foreach (var result in importResults)
                {
                    Console.WriteLine(result?.ToString());
                }
            }

            // Save the updated PDF document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and PDF saved to '{outputPdfPath}'.");
    }
}
