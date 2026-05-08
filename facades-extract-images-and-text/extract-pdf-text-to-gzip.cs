using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputGzipPath = "output.txt.gz";    // compressed text file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor is a Facade; use using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Open the output file stream
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            // Wrap the file stream with GZipStream for compression
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Write the extracted text directly into the compressed stream
                extractor.GetText(gzipStream);
            }
        }

        Console.WriteLine($"Text extracted and compressed to '{outputGzipPath}'.");
    }
}