using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare options for JSON export (optional: indented output)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Create the output file stream and wrap it with GZipStream for compression
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Export all form fields to JSON directly into the compressed stream
                doc.Form.ExportToJson(gzipStream, jsonOptions);
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}