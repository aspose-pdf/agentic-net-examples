using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfExtractor is a Facades class that can extract text from a PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file.
            extractor.BindPdf(inputPath);

            // Perform text extraction.
            extractor.ExtractText();

            // Store the extracted text in a MemoryStream.
            using (MemoryStream ms = new MemoryStream())
            {
                // The Aspose API expects a non‑null stream. The null‑forgiving operator
                // guarantees the compiler that 'ms' is not null, silencing CS8600.
                extractor.GetText(ms!);
                ms.Position = 0; // Ensure the stream is at the beginning.

                // If the stream length is greater than zero, the PDF contains text.
                bool hasText = ms.Length > 0;

                Console.WriteLine(hasText
                    ? "PDF contains text."
                    : "PDF does not contain any text.");
            }
        }
    }
}
