using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newKeywords = "Accessible; Example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a stream and initialize PdfFileInfo with it
        using (FileStream pdfStream = File.OpenRead(inputPath))
        {
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfStream);

            // Update the Keywords metadata
            pdfInfo.Keywords = newKeywords;

            // Save the PDF with the updated metadata
            pdfInfo.Save(outputPath);
        }

        Console.WriteLine($"Keywords updated and saved to '{outputPath}'.");
    }
}