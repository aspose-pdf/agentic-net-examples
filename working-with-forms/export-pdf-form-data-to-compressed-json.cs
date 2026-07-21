using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for input PDF and compressed JSON output
        const string inputPdfPath = "input.pdf";
        const string outputGzipPath = "formdata.json.gz";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the output file stream for the .gz file
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            // Wrap the file stream with GZipStream to compress data on the fly
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Export all form fields to JSON directly into the GZipStream.
                // The optional ExportFieldsToJsonOptions parameter is omitted (defaults to null).
                pdfDoc.Form.ExportToJson(gzipStream);
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}