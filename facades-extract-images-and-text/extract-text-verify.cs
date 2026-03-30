using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFile = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract text using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractText();

            // Get text into a memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                extractor.GetText(memoryStream);
                memoryStream.Position = 0;
                string textFromMemory = Encoding.Unicode.GetString(memoryStream.ToArray());

                // Save extracted text to a file
                extractor.GetText(outputFile);

                // Read back the saved file
                string textFromFile = File.ReadAllText(outputFile, Encoding.Unicode);

                // Verify that both texts are identical
                bool isMatch = string.Equals(textFromMemory, textFromFile, StringComparison.Ordinal);
                Console.WriteLine(isMatch ? "Extracted text matches the saved file." : "Mismatch between extracted text and saved file.");
            }
        }
    }
}
