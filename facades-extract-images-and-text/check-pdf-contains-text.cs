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

        bool containsText = false;

        // Use PdfExtractor (Facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // Extract all text (Unicode encoding is default)
            extractor.ExtractText();

            // Write the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                // If the stream length is greater than zero, the PDF had text
                containsText = textStream.Length > 0;
            }
        }

        Console.WriteLine($"PDF contains text: {containsText}");
    }
}