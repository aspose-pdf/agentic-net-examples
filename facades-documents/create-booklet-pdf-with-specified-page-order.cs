using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (booklet)
        const string outputPath = "booklet.pdf";

        // Define left and right page sequences for the booklet
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5 };
        int[] rightPages = new int[] { 6, 7, 8, 9, 10 };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade and generate the booklet
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        // Report the outcome
        if (result)
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}