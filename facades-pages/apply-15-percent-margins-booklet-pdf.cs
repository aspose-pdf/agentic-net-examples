using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output_booklet.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Define the pages to which the margin should be applied.
        // Use null to apply to all pages, or specify an array of 1‑based page numbers.
        // Example: int[] pages = new int[] { 1, 2, 3, 4 };
        int[] pages = null; // apply to all pages

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Apply a 15 % margin on each side (left, right, top, bottom).
        // Margins are specified as percentages of the original page size.
        bool success = fileEditor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: pages,
            leftMargin: 15,   // 15 % left margin
            rightMargin: 15,  // 15 % right margin
            topMargin: 15,    // 15 % top margin
            bottomMargin: 15  // 15 % bottom margin
        );

        // Report the result
        if (success)
        {
            Console.WriteLine($"Successfully created booklet PDF with 15 % margins: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to apply margins to the PDF.");
        }
    }
}