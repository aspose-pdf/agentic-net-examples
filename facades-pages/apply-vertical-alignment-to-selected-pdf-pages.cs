using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the pages you want to align (1‑based indexing)
        int[] selectedPages = new int[] { 1, 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page layout
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Specify which pages to process
            editor.ProcessPages = selectedPages;

            // Align the original content to the top of each selected page
            // Use the correct property and enum for vertical alignment
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Save the modified PDF (no ApplyChanges call needed)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied. Saved to '{outputPath}'.");
    }
}
