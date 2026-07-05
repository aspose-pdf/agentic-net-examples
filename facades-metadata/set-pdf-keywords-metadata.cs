using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileInfo resides here
using Aspose.Pdf;          // optional, for Document if needed later

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keywords   = "Aspose PDF, Metadata, Keywords";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the Keywords metadata, and save the updated file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Keywords = keywords;                     // set Keywords field
            bool saved = pdfInfo.SaveNewInfo(outputPath);    // persist changes
            Console.WriteLine(saved ? "Keywords saved successfully." : "Failed to save Keywords.");
        }

        // Verify that the Keywords were written by reading them back
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPath))
        {
            Console.WriteLine($"Keywords after save: {verifyInfo.Keywords}");
        }
    }
}