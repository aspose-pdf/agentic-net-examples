using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp instance, bind the source PDF, add page numbers with leading zeros, and save.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        // Format string "Page 000#" adds leading zeros (e.g., Page 001, Page 002, ...).
        fileStamp.AddPageNumber("Page 000#");
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}