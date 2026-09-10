using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base PDF into which pages will be inserted
        const string basePdfPath = "base.pdf";

        // Source PDFs and the page range to take from each of them
        string[] sourcePdfPaths = { "source1.pdf", "source2.pdf", "source3.pdf" };
        const int startPage = 2; // inclusive, 1‑based indexing
        const int endPage   = 5; // inclusive

        // Resulting PDF after insertion
        const string outputPdfPath = "output.pdf";

        // Validate file existence
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base file not found: {basePdfPath}");
            return;
        }

        foreach (var src in sourcePdfPaths)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source file not found: {src}");
                return;
            }
        }

        // Use PdfFileEditor to append pages from multiple source PDFs in one operation
        PdfFileEditor editor = new PdfFileEditor();

        // TryAppend inserts the specified page range from each source PDF and appends them to the base PDF.
        // The method returns false instead of throwing if the operation fails.
        bool success = editor.TryAppend(basePdfPath, sourcePdfPaths, startPage, endPage, outputPdfPath);

        if (success)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
            if (editor.LastException != null)
                Console.Error.WriteLine($"Error details: {editor.LastException.Message}");
        }
    }
}