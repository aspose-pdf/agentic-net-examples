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

        // Use PdfExtractor (facade) to extract text.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // Extract text from the whole document using Unicode encoding.
            extractor.ExtractText(); // default is Unicode

            // Save the extracted text into a memory stream.
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Determine if any text was extracted.
                bool containsText = textStream.Length > 0;

                Console.WriteLine(containsText
                    ? "The PDF contains text."
                    : "The PDF does not contain any text.");
            }

            // Close the extractor (optional, as using will dispose it).
            extractor.Close();
        }
    }
}