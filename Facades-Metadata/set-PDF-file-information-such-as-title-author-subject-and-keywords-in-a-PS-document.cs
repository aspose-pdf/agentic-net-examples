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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set standard metadata fields
            pdfInfo.Title    = "Sample Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "keyword1, keyword2";

            // Save the updated metadata to a new PDF file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}