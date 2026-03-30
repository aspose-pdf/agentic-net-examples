using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_text.gz";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractText();
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                {
                    extractor.GetText(gzipStream);
                }
            }
        }

        Console.WriteLine("Extracted text saved to compressed GZip file: " + outputPath);
    }
}