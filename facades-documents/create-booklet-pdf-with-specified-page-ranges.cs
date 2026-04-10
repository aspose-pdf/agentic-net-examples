using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define left and right page sequences for the booklet
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5 };
        int[] rightPages = new int[] { 6, 7, 8, 9, 10 };

        try
        {
            // Create the PdfFileEditor and generate the booklet
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

            if (success)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}