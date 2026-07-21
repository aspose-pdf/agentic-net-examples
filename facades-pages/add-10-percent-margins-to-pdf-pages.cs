using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (100‑page document) and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (no IDisposable implementation)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add a 10 % margin on all four sides of every page.
        // Passing null for the pages array processes all pages in the document.
        const double marginPercent = 10.0; // 10 % margin
        bool success = fileEditor.AddMarginsPct(
            inputPath,          // source PDF
            outputPath,         // destination PDF
            null,               // null = all pages
            marginPercent,      // left margin (% of page width)
            marginPercent,      // right margin (% of page width)
            marginPercent,      // top margin (% of page height)
            marginPercent);     // bottom margin (% of page height)

        if (success)
        {
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF contents.");
        }
    }
}