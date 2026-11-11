using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the number of pages in the source PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based page count
        }

        // Build an array containing all page numbers (1‑based)
        int[] allPages = new int[pageCount];
        for (int i = 0; i < pageCount; i++)
            allPages[i] = i + 1;

        // Reduce the content size to 90 % (10 % margin) for every page
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = allPages; // apply to all pages
            editor.Zoom = 0.9f;              // 90 % scaling
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}