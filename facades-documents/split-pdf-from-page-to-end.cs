using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (contains pages from startPage to the end)
        const string outputPath = "output.pdf";
        // Page from which to start the split (1‑based indexing)
        const int startPage = 5;

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF with Document to obtain page count (PdfFileEditor has no GetPageCount)
        int totalPages;
        using (Document pdfDoc = new Document(inputPath))
        {
            totalPages = pdfDoc.Pages.Count;
        }

        // Validate start page range (Aspose.Pdf uses 1‑based page numbers)
        if (startPage < 1 || startPage > totalPages)
        {
            Console.Error.WriteLine($"Start page {startPage} is out of range (1‑{totalPages}).");
            return;
        }

        // PdfFileEditor provides file‑level operations (no IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Use SplitToBulks – it accepts the input path and an array of page‑range pairs.
        // We need a single range: startPage .. totalPages.
        int[][] pageRanges = new int[][] { new int[] { startPage, totalPages } };
        MemoryStream[] bulks = editor.SplitToBulks(inputPath, pageRanges);

        if (bulks == null || bulks.Length == 0)
        {
            Console.Error.WriteLine("No pages were extracted.");
            return;
        }

        // The first (and only) bulk contains the requested pages. Save it to the output file.
        using (MemoryStream ms = bulks[0])
        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            ms.Position = 0; // ensure we start from the beginning
            ms.CopyTo(fs);
        }

        Console.WriteLine($"Pages {startPage}‑{totalPages} saved to '{outputPath}'.");
    }
}
