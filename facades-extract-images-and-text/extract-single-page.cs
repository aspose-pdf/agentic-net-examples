using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page1.pdf";
        const int pageNumber = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Extract(inputPath, pageNumber, pageNumber, outputPath);
        if (success)
        {
            Console.WriteLine($"Page {pageNumber} extracted to {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Extraction failed.");
        }
    }
}