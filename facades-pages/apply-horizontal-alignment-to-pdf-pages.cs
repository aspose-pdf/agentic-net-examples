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
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the total number of pages in the document.
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Create a 1‑based array that contains every page number.
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Apply left‑justified (horizontal) alignment to all pages.
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = allPages;                 // <-- specify target pages
            editor.HorizontalAlignment = HorizontalAlignment.Left; // <-- new property
            editor.Save(outputPath);
        }

        Console.WriteLine($"All pages left‑aligned and saved to '{outputPath}'.");
    }
}
