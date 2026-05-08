using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keywords   = "example, Aspose, PDF";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the Keywords metadata, and save the updated file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Keywords = keywords;
            bool success = pdfInfo.SaveNewInfo(outputPath);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save the PDF with updated metadata.");
                return;
            }
        }

        // Verify that the Keywords were written correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPath))
        {
            Console.WriteLine($"Keywords after save: {verifyInfo.Keywords}");
        }
    }
}