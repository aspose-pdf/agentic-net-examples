using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keywords   = "Aspose.Pdf, Metadata, Keywords";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Set the Keywords metadata using PdfFileInfo (Facades API)
        // -----------------------------------------------------------------
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);
        pdfInfo.Keywords = keywords;          // set the Keywords field
        pdfInfo.Save(outputPath);             // save to a new PDF file

        // -----------------------------------------------------------------
        // Verify that the Keywords were saved correctly
        // -----------------------------------------------------------------
        PdfFileInfo verifyInfo = new PdfFileInfo(outputPath);
        string savedKeywords = verifyInfo.Keywords ?? string.Empty;

        Console.WriteLine($"Keywords set:    \"{keywords}\"");
        Console.WriteLine($"Keywords saved: \"{savedKeywords}\"");

        if (string.Equals(keywords, savedKeywords, StringComparison.Ordinal))
        {
            Console.WriteLine("Verification succeeded: Keywords match.");
        }
        else
        {
            Console.WriteLine("Verification failed: Keywords do not match.");
        }
    }
}