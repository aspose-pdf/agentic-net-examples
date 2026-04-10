using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define left and right page sequences for the booklet
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] rightPages = new int[] {11,12,13,14,15,16,17,18,19,20};

        try
        {
            // Create the facade and generate the customized booklet
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

            if (result)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}