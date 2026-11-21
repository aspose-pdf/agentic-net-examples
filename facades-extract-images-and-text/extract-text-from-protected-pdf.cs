using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "extracted.txt";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so use a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF (no password overload) and then supply the owner password
                extractor.BindPdf(inputPath);
                extractor.Password = ownerPassword; // full‑permission access

                // Extract text from all pages
                extractor.ExtractText();

                // Write the extracted text to a plain‑text file
                extractor.GetText(outputPath);
            }

            Console.WriteLine($"Text successfully extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
