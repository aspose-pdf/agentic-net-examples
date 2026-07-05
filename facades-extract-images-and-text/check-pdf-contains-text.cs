using System;
using System.IO;
using System.Text;
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

        // Initialize the PdfExtractor facade and bind the PDF file.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            // Extract all text using Unicode encoding.
            extractor.ExtractText(Encoding.Unicode);

            // Write the extracted text to a memory stream.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Determine if any text was extracted.
                bool containsText = textStream.Length > 0;

                Console.WriteLine(containsText
                    ? "The PDF contains text."
                    : "The PDF does not contain any text.");
            }
        }
    }
}