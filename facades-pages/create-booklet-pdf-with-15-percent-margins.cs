using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_with_margins.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Determine all page numbers (Aspose.Pdf uses 1‑based indexing)
        // ------------------------------------------------------------
        int[] allPages;
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                allPages[i] = i + 1; // 1‑based page numbers
        }

        // ------------------------------------------------------------
        // Step 2: Apply a 15 % margin on each side of the selected pages.
        // The AddMarginsPct method works with percentages of the original
        // page size, so we pass 15 for left, top, right and bottom.
        // ------------------------------------------------------------
        double marginPercent = 15.0;

        // Create a temporary file to hold the margin‑adjusted PDF.
        string tempPath = Path.Combine(Path.GetTempPath(),
                                       Guid.NewGuid().ToString() + ".pdf");

        PdfFileEditor marginEditor = new PdfFileEditor();
        bool marginsApplied = marginEditor.AddMarginsPct(
            inputPath,          // source PDF
            tempPath,           // destination PDF with margins
            allPages,           // pages to process
            marginPercent,      // left margin (%)
            marginPercent,      // top margin (%)
            marginPercent,      // right margin (%)
            marginPercent       // bottom margin (%)
        );

        if (!marginsApplied)
        {
            Console.Error.WriteLine("Failed to apply margins.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Convert the margin‑adjusted PDF into a booklet layout.
        // The MakeBooklet method reorders pages for booklet printing.
        // ------------------------------------------------------------
        PdfFileEditor bookletEditor = new PdfFileEditor();
        bool bookletCreated = bookletEditor.MakeBooklet(
            tempPath,   // input with margins
            outputPath  // final booklet PDF
        );

        // Clean up the temporary file.
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine(bookletCreated
            ? $"Booklet PDF with 15 % margins saved to '{outputPath}'."
            : "Failed to create booklet PDF.");
    }
}