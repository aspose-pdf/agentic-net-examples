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

        int[] leftPages = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] rightPages = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
            if (success)
            {
                Console.WriteLine($"Booklet created: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("Failed to create booklet.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
