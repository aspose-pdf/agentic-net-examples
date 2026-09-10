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

        // Extract text and write it directly to a GZip compressed stream
        using (PdfExtractor extractor = new PdfExtractor())
        using (FileStream gzipFileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
        using (GZipStream gzipStream = new GZipStream(gzipFileStream, CompressionLevel.Optimal))
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractText();               // Extract all text from the PDF
            extractor.GetText(gzipStream);         // Write extracted text to the GZip stream
        }

        Console.WriteLine($"Text extracted and compressed to '{outputGzipPath}'.");
    }
}