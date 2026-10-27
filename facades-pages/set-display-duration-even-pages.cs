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
        const string outputPath = "output.pdf";
        const int evenPageDuration = 5; // seconds for even‑numbered pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Work on a copy to keep the original unchanged
        File.Copy(inputPath, outputPath, true);

        // Determine total page count using Document (PdfPageEditor has no GetPageCount)
        int pageCount;
        using (var doc = new Document(outputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array of even‑numbered page indices (1‑based)
        int[] evenPages = Enumerable.Range(1, pageCount)
                                    .Where(p => p % 2 == 0)
                                    .ToArray();

        // Use PdfPageEditor to set the display duration for the selected pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(outputPath);
            editor.ProcessPages = evenPages;          // specify target pages
            editor.DisplayDuration = evenPageDuration; // seconds
            editor.ApplyChanges();                    // apply the changes to the selected pages
            editor.Save(outputPath);                  // overwrite the copy
        }

        Console.WriteLine($"Display duration set for even pages. Output saved to '{outputPath}'.");
    }
}
