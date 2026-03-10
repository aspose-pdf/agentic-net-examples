using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (result after deletion)
        const string outputPath = "output.pdf";
        // Pages to delete – 1‑based indexing as required by Aspose.Pdf
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade editor and perform the deletion
        PdfFileEditor editor = new PdfFileEditor();
        bool succeeded = editor.Delete(inputPath, pagesToDelete, outputPath);

        // Report the result
        if (succeeded)
        {
            Console.WriteLine($"Specified pages removed. Updated file saved as '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
        }
    }
}