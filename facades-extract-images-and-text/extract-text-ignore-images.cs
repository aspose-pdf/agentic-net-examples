using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            // Configure extractor to ignore images by not invoking ExtractImage.
            // Setting the mode explicitly (default is DefinedInResources) makes the intent clear.
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
            extractor.ExtractText();
            extractor.GetText(outputPath);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}