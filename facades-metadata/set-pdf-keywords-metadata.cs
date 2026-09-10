using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Set the Keywords metadata and save the updated PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Keywords = "Sample, Aspose, PDF";
            bool success = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(success ? "Keywords set and PDF saved." : "Failed to save updated PDF.");
        }

        // Verify that the Keywords were written correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPath))
        {
            Console.WriteLine($"Keywords after save: {verifyInfo.Keywords}");
        }
    }
}