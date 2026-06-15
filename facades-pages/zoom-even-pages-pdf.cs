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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that edits pages (zoom, rotate, etc.)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Select even‑numbered pages only
            int[] evenPages = Enumerable.Range(1, pageCount)
                                        .Where(p => p % 2 == 0)
                                        .ToArray();

            // Restrict editing to the even pages
            editor.ProcessPages = evenPages;

            // Set zoom factor to 0.8 (80 % of original size)
            editor.Zoom = 0.8f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages zoomed to 0.8 and saved as '{outputPath}'.");
    }
}