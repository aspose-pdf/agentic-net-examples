using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting booklet PDF
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Define left and right page sequences for the booklet
        int[] leftPages = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] rightPages = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        // Verify that the input file exists before proceeding
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
        {
            Console.WriteLine($"Booklet successfully created at '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}