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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Extract text and write it directly into a GZip stream
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdfPath);

            // Extract all text (Unicode encoding by default)
            extractor.ExtractText();

            // Create the output file stream
            using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
            // Wrap it with GZip compression
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                // Save extracted text into the compressed stream
                extractor.GetText(gzipStream);
            }
        }

        Console.WriteLine($"Text extracted and compressed to '{outputGzipPath}'.");
    }
}