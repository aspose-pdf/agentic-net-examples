using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing at least 20 pages
        const string inputPath = "input.pdf";
        // Output PDF that will contain the booklet layout
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define left and right page sequences for the booklet
        int[] leftPages = new int[10];
        int[] rightPages = new int[10];
        for (int i = 0; i < 10; i++)
        {
            leftPages[i] = i + 1;   // Pages 1‑10 on the left side
            rightPages[i] = i + 11; // Pages 11‑20 on the right side
        }

        // Create the PdfFileEditor and generate the booklet
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        if (result)
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}