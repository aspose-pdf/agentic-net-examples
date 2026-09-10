using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
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