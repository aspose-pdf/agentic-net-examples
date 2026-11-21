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
        const string outputPath = "shrinked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document to obtain the total number of pages.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Prepare an array with all page numbers (1‑based).
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Use PdfPageEditor for page‑level modifications.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Apply the operation to every page.
            editor.ProcessPages = allPages;

            // Shrink page contents uniformly by 5 % on each side (overall 90 % scale).
            // The Zoom property expects a float where 1.0 = 100 %.
            editor.Zoom = 0.90f; // 5 % margin on each side → 90 % of original size.

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
