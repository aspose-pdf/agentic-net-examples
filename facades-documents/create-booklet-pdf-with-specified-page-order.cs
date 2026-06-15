using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define left and right page sequences for the booklet
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5 };
        int[] rightPages = new int[] { 6, 7, 8, 9, 10 };

        // Create the editor and generate the booklet with default page size
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        Console.WriteLine(result
            ? $"Booklet created successfully: {outputPath}"
            : "Failed to create booklet.");
    }
}