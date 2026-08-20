using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Add a footer 10 points above the bottom edge of each page
            fileStamp.AddFooter(new FormattedText("Footer text"), 10);

            // Save the stamped PDF
            fileStamp.Save(outputPath);
            fileStamp.Close(); // optional, Dispose() will also close
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}