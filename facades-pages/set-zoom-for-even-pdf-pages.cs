using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page properties.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Determine total pages and select even‑numbered pages (1‑based indexing).
            int totalPages = editor.GetPages();
            int[] evenPages = Enumerable.Range(1, totalPages)
                                        .Where(p => p % 2 == 0)
                                        .ToArray();

            // Apply changes only to the even pages.
            editor.ProcessPages = evenPages;

            // Set zoom factor to 0.8 (80% of original size).
            editor.Zoom = 0.8f;

            // Apply the modifications.
            editor.ApplyChanges();

            // Save the result. No SaveOptions needed; the output will be PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages zoomed to 0.8 and saved as '{outputPath}'.");
    }
}