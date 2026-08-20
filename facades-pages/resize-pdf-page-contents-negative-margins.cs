using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Open source and destination streams inside using blocks for deterministic disposal
        using (FileStream srcStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream dstStream  = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Apply a negative margin of 5% on all sides.
            // Negative margins shrink the page contents uniformly.
            // Passing null for the pages array processes all pages.
            bool success = editor.AddMarginsPct(
                srcStream,
                dstStream,
                null,   // all pages
                -5,     // left margin  (-5%)
                -5,     // right margin (-5%)
                -5,     // top margin   (-5%)
                -5);    // bottom margin(-5%)

            if (!success)
            {
                Console.Error.WriteLine("Failed to resize page contents.");
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}