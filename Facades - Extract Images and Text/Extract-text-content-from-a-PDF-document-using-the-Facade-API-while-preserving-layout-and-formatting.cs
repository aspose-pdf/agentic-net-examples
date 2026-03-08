using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor is a Facade that extracts text (and images) from a PDF.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Initialize the facade with the source PDF.
            extractor.BindPdf(inputPdf);

            // ExtractTextMode:
            // 0 – Pure text mode (preserves formatting such as line breaks).
            // 1 – Raw ordering mode (preserves the original order of operators).
            // For layout‑preserving extraction we use the pure mode.
            extractor.ExtractTextMode = 0;

            // Perform the extraction.
            extractor.ExtractText();

            // Write the extracted text to a file.
            extractor.GetText(outputTxt);
        }

        Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
    }
}