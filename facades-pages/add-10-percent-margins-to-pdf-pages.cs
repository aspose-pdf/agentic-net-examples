using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the facade that provides page‑editing operations
        PdfFileEditor editor = new PdfFileEditor();

        // Add a 10 % margin on all four sides for every page.
        // Passing null for the pages array tells the method to process all pages.
        bool result = editor.AddMarginsPct(
            inputPath,      // source document path
            outputPath,     // destination document path
            null,           // process all pages
            10,             // left margin (percent of page width)
            10,             // right margin (percent of page width)
            10,             // top margin (percent of page height)
            10              // bottom margin (percent of page height)
        );

        // Report the outcome
        if (result)
            Console.WriteLine($"Successfully resized PDF. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF.");
    }
}