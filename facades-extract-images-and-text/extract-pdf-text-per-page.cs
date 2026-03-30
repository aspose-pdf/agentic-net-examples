using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPrefix = "page";
        const string outputSuffix = ".txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.ExtractText(Encoding.Unicode);

        int pageNumber = 1;
        while (extractor.HasNextPageText())
        {
            string outputFile = outputPrefix + pageNumber + outputSuffix;
            extractor.GetNextPageText(outputFile);
            Console.WriteLine("Saved page " + pageNumber + " text to " + outputFile);
            pageNumber++;
        }
    }
}