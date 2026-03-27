using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        int startPage = 3; // page number to start splitting from (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);
        if (success)
        {
            Console.WriteLine($"PDF split from page {startPage} to end saved as '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to split the PDF document.");
        }
    }
}