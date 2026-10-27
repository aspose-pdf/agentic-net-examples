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
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine total number of pages using Document (PdfPageEditor no longer exposes page‑count helpers)
        var document = new Document(inputPath);
        int pageCount = document.Pages.Count;
        int[] allPages = Enumerable.Range(1, pageCount).ToArray(); // 1‑based page numbers

        // Apply a uniform 5 % shrink to the page contents via Zoom
        var editor = new PdfPageEditor();
        editor.BindPdf(inputPath);
        editor.ProcessPages = allPages;   // target every page
        editor.Zoom = 0.95f;               // shrink content by 5 %
        editor.Save(outputPath);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
