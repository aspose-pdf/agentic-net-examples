using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_booklet.pdf";

        // Define the pages to which the margin should be applied.
        // Use null to process all pages, or specify an array of 1‑based page numbers.
        // Example: apply to pages 1, 2 and 3 only:
        // int[] pages = new int[] { 1, 2, 3 };
        int[] pages = null; // null = all pages

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Apply a 15 % margin on each side of the selected pages.
        // Margins are specified as percentages of the original page size.
        bool success = editor.AddMarginsPct(
            inputPdf,          // source PDF path
            outputPdf,         // destination PDF path
            pages,             // pages to process (null = all)
            15,                // left margin 15 %
            15,                // right margin 15 %
            15,                // top margin 15 %
            15);               // bottom margin 15 %

        if (success)
        {
            Console.WriteLine($"Margins applied successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to apply margins.");
        }
    }
}