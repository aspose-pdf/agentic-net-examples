using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes: PdfFileEditor

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdf   = "source.pdf";          // Original PDF
        const string nupPdf      = "nup_intermediate.pdf"; // N‑up temporary file
        const string bookletPdf  = "booklet_output.pdf";   // Final booklet

        // Verify source exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // ---------------------------------------------------------
            // Step 1: Create an N‑up layout.
            //    - 2 columns (x) and 2 rows (y) => 4 pages per sheet.
            //    - Output written to nupPdf.
            // ---------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();
            bool nupResult = editor.MakeNUp(sourcePdf, nupPdf, 2, 2);
            if (!nupResult)
            {
                Console.Error.WriteLine("N‑up operation failed.");
                return;
            }

            // ---------------------------------------------------------
            // Step 2: Convert the N‑up PDF into a booklet.
            //    - Input is the N‑up PDF created above.
            //    - Output is the final booklet PDF.
            // ---------------------------------------------------------
            bool bookletResult = editor.MakeBooklet(nupPdf, bookletPdf);
            if (!bookletResult)
            {
                Console.Error.WriteLine("Booklet creation failed.");
                return;
            }

            // Optional: clean up the intermediate N‑up file
            try { File.Delete(nupPdf); } catch { /* ignore cleanup errors */ }

            Console.WriteLine($"Booklet created successfully: {bookletPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}