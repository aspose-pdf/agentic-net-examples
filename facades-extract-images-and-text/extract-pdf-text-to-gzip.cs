using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output GZip file path (compressed text)
        const string gzipPath = "output.txt.gz";

        // Ensure the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor facade to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding by default)
            extractor.ExtractText();

            // Create the output file stream for the compressed data
            using (FileStream fileStream = new FileStream(gzipPath, FileMode.Create, FileAccess.Write))
            // Wrap the file stream with GZipStream for compression
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                // Write the extracted text directly into the GZip stream
                extractor.GetText(gzipStream);
            }
        }

        Console.WriteLine($"Text extracted and compressed to: {gzipPath}");
    }
}