using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base PDF into which pages will be inserted
        const string basePdf = "base.pdf";

        // Source PDFs from which pages will be taken
        string[] sourcePdfs = { "source1.pdf", "source2.pdf", "source3.pdf" };

        // Define the page range to extract from each source PDF (inclusive, 1‑based)
        int startPage = 2; // first page to take from each source
        int endPage   = 5; // last page to take from each source

        // Output PDF that will contain the base PDF plus the inserted pages
        const string outputPdf = "merged_output.pdf";

        // Verify that all files exist before proceeding
        if (!File.Exists(basePdf))
        {
            Console.Error.WriteLine($"Base file not found: {basePdf}");
            return;
        }

        foreach (string src in sourcePdfs)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source file not found: {src}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

            // Append pages from each source PDF (within the same range) to the end of the base PDF
            // This operation creates a new PDF file; the original files remain unchanged.
            bool success = editor.Append(basePdf, sourcePdfs, startPage, endPage, outputPdf);

            if (success)
                Console.WriteLine($"Pages inserted successfully. Result saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Page insertion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}