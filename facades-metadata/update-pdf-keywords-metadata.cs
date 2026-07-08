using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newKeywords = "example, Aspose, PDF";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a stream and bind it to PdfFileInfo
        using (FileStream pdfStream = File.OpenRead(inputPath))
        using (PdfFileInfo info = new PdfFileInfo(pdfStream))
        {
            // Update the Keywords metadata
            info.Keywords = newKeywords;

            // Save the updated PDF to a new file
            info.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Keywords updated and saved to '{outputPath}'.");
    }
}