using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_even_scaled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get total number of pages using Document (PdfPageEditor does not expose PageCount)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array of even‑numbered page indices (1‑based)
        int[] evenPages = Enumerable.Range(1, pageCount)
                                    .Where(p => p % 2 == 0)
                                    .ToArray();

        // Apply a 1.2 (120%) zoom to the selected even pages
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = evenPages;   // specify target pages
            editor.Zoom = 1.2f;                // set zoom factor
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even‑numbered pages scaled and saved to '{outputPath}'.");
    }
}
