using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        // Specify which pages to resize.
        // Use null to process all pages, or provide an array of 1‑based page numbers.
        int[] pages = null; // all pages

        // Define margins as 15 % of the original page size on each side.
        const double leftMargin   = 15; // percent
        const double rightMargin  = 15; // percent
        const double topMargin    = 15; // percent
        const double bottomMargin = 15; // percent

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Apply the percentage‑based margins to the selected pages.
        bool success = editor.AddMarginsPct(
            inputPath,
            outputPath,
            pages,
            leftMargin,
            rightMargin,
            topMargin,
            bottomMargin);

        // Report the outcome.
        if (success)
        {
            Console.WriteLine($"Margins applied successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to apply margins.");
        }
    }
}