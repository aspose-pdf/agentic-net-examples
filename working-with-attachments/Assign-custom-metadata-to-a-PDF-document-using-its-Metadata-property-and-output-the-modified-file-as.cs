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

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Add custom metadata entries
            pdfInfo.SetMetaInfo("CustomKey1", "CustomValue1");
            pdfInfo.SetMetaInfo("CustomKey2", "CustomValue2");

            // Save the PDF with the updated metadata
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"PDF with custom metadata saved to '{outputPath}'.");
    }
}