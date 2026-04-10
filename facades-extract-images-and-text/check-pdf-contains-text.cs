using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

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

        // Initialize the extractor and bind the PDF file
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding (optional, default is Unicode)
            extractor.ExtractText(Encoding.Unicode);

            // Save extracted text into a memory stream
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