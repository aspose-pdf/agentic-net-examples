using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths for the source PDF, intermediate N‑up PDF, and final booklet PDF
        const string sourcePdf   = "input.pdf";
        const string nupPdf      = "temp_nup.pdf";
        const string bookletPdf  = "output_booklet.pdf";

        // Verify source file exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Create an N‑up layout (e.g., 2 columns × 2 rows)
        // ------------------------------------------------------------
        // PdfFileEditor does NOT implement IDisposable, so we instantiate it normally.
        PdfFileEditor editor = new PdfFileEditor();

        // MakeNUp overload: (inputFile, outputFile, columns, rows)
        // This will place 4 original pages on each new page.
        bool nupResult = editor.MakeNUp(sourcePdf, nupPdf, 2, 2);
        if (!nupResult)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Convert the N‑up PDF into a booklet
        // ------------------------------------------------------------
        // Re‑use the same PdfFileEditor instance (or create a new one).
        bool bookletResult = editor.MakeBooklet(nupPdf, bookletPdf);
        if (!bookletResult)
        {
            Console.Error.WriteLine("Failed to create booklet PDF.");
            return;
        }

        // Clean up the intermediate N‑up file if desired
        try { File.Delete(nupPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Booklet PDF created successfully: {bookletPdf}");
    }
}