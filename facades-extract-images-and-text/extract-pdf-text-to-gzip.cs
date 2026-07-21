using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputGz = "output.txt.gz";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PDF extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract all text using Unicode encoding
            extractor.ExtractText();

            // Create the output file stream and wrap it with GZip compression
            using (FileStream fileStream = new FileStream(outputGz, FileMode.Create, FileAccess.Write))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
            {
                // Write the extracted text directly into the compressed stream
                extractor.GetText(gzipStream);
            }
        }

        Console.WriteLine($"Extracted text saved to compressed file: {outputGz}");
    }
}