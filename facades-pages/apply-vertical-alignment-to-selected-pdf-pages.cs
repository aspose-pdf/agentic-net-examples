using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page layout.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify the pages to which the vertical alignment will be applied.
            // Example: pages 2, 3, and 5 (1‑based indexing).
            editor.ProcessPages = new int[] { 2, 3, 5 };

            // Align the original content to the top of the result page.
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Apply the changes to the selected pages.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied. Output saved to '{outputPath}'.");
    }
}