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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Obtain total page count via Document (PdfPageEditor does not expose PageCount)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array of even‑numbered page indices (1‑based as required by PdfPageEditor)
        int[] evenPages = Enumerable.Range(1, pageCount)
                                    .Where(p => p % 2 == 0)
                                    .ToArray();

        // Apply a 1.2 (120 %) zoom factor to the selected pages using ProcessPages and Zoom
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = evenPages;   // target pages
            editor.Zoom = 1.2f;                // zoom factor
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages scaled to 1.2× and saved to '{outputPath}'.");
    }
}
