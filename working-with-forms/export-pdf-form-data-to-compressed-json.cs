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

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create the output file stream for the compressed JSON
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            // Wrap the file stream with GZipStream to compress the JSON data on the fly
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Configure JSON export options (optional)
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = true // produce readable JSON before compression
                };

                // Export all form fields to JSON, writing directly into the GZip stream
                doc.Form.ExportToJson(gzipStream, jsonOptions);
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}