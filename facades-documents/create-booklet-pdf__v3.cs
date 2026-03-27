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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int[] leftPages = new int[] { 1, 2, 3, 4, 5 };
        int[] rightPages = new int[] { 6, 7, 8, 9, 10 };

        PdfFileEditor pdfEditor = new PdfFileEditor();
        bool success = pdfEditor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
        if (success)
        {
            Console.WriteLine($"Booklet created: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}