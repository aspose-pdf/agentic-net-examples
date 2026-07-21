using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing at least 20 pages
        const string inputPath  = "input.pdf";
        // Output PDF will be the booklet
        const string outputPath = "booklet.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define left and right page sequences for the booklet
        // Left side pages: 1 through 10
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        // Right side pages: 11 through 20
        int[] rightPages = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Generate the customized booklet
        bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        if (success)
        {
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }

        // No need to dispose PdfFileEditor (it does not implement IDisposable)
    }
}