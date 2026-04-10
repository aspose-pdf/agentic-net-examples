using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputGzipPath = "extracted_text.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the PdfExtractor facade and bind the PDF document
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Create the output file stream and wrap it with GZipStream for compression
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Write the extracted text directly into the compressed stream
                extractor.GetText(gzipStream);
            }
        }

        Console.WriteLine($"Text extracted and compressed to '{outputGzipPath}'.");
    }
}