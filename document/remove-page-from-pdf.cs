using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, PageCollection, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // PDF after page removal
        const int    pageToDelete = 3;              // 1‑based page number to remove

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, delete the specified page, and save the result.
        // Document implements IDisposable – use a using block for deterministic cleanup.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists.
            if (pageToDelete < 1 || pageToDelete > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageToDelete} is out of range (1‑{doc.Pages.Count}).");
                return;
            }

            // Delete the page. Page numbers are 1‑based, so this call removes the exact page.
            doc.Pages.Delete(pageToDelete);

            // Remaining pages are automatically renumbered by Aspose.Pdf.
            doc.Save(outputPath);   // Save as PDF (no SaveOptions needed for PDF output)
        }

        Console.WriteLine($"Page {pageToDelete} removed. Result saved to '{outputPath}'.");
    }
}