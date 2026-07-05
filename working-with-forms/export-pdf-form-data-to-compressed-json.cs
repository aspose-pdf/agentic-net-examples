using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputGzipPath = "formdata.json.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare options for JSON export (no indentation to keep size small)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = false
            };

            // Open the target file and wrap it with GZipStream for compression
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                // Export all form fields to JSON and write directly into the compressed stream
                pdfDoc.Form.ExportToJson(gzipStream, jsonOptions);
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}