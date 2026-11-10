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

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text.
        // The class implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // Enable text extraction.
            extractor.ExtractText();

            // Extract the text into a MemoryStream.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // After extraction, the stream position is at the end.
                // Length indicates the number of bytes extracted.
                bool containsText = textStream.Length > 0;

                Console.WriteLine(containsText
                    ? "The PDF contains text."
                    : "The PDF does not contain any text.");
            }
        }
    }
}