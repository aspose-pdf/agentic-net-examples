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

        bool hasText = false;

        using (FileStream fileStream = File.OpenRead(inputPath))
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(fileStream);
            extractor.ExtractText();

            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                hasText = textStream.Length > 0;
            }
        }

        Console.WriteLine(hasText ? "PDF contains text." : "PDF does not contain any text.");
    }
}