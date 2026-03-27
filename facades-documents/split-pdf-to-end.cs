using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int startPage = 5; // split from this page to the end

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor pdfEditor = new PdfFileEditor();
        bool success = pdfEditor.SplitToEnd(inputPath, startPage, outputPath);
        if (success)
        {
            Console.WriteLine($"Successfully split PDF from page {startPage} to end. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to split the PDF.");
        }
    }
}
