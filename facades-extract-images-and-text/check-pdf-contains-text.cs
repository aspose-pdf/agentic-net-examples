using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract text using default Unicode encoding
            extractor.ExtractText();

            // Store extracted text in a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Determine if any text was extracted
                bool containsText = textStream.Length > 0;

                Console.WriteLine(containsText
                    ? "The PDF contains text."
                    : "The PDF does not contain any text.");
            }
        }
    }
}