using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with form fields
        const string outputGzipPath = "formdata.json.gz"; // compressed JSON output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare GZip stream that writes compressed data to the output file
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Optional: configure JSON export options (e.g., no indentation to reduce size)
                ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
                {
                    WriteIndented = false   // compact JSON
                };

                // Export all form fields to JSON directly into the GZip stream
                pdfDoc.Form.ExportToJson(gzipStream, jsonOptions);
            }
        }

        Console.WriteLine($"Form data exported and compressed to '{outputGzipPath}'.");
    }
}