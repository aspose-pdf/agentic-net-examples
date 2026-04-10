using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the pages to delete (2 through 5)
        int[] pagesToDelete = new int[] { 2, 3, 4, 5 };

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages 2-5 have been removed. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
        }
    }
}