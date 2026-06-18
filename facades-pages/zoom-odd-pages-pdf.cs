using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Determine total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Build an array of odd‑numbered page indexes
            int[] oddPages = Enumerable.Range(1, pageCount)
                                       .Where(p => p % 2 == 1)
                                       .ToArray();

            // Restrict editing to the odd pages only
            editor.ProcessPages = oddPages;

            // Set zoom factor (1.0 = 100%). 1.2 = 120%
            editor.Zoom = 1.2f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to odd pages. Output saved to '{outputPath}'.");
    }
}