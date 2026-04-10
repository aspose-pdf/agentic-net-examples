using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the intermediate N‑up PDF, and the final booklet PDF
        const string inputPdf   = "input.pdf";
        const string nupPdf     = "temp_nup.pdf";
        const string bookletPdf = "booklet_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Create an N‑up layout.
            // ------------------------------------------------------------
            // The MakeNUp method arranges multiple pages of the source PDF
            // onto a single page. Here we use a 2 × 2 grid (4‑up) which
            // reduces the page count and prepares the document for booklet
            // pagination.
            PdfFileEditor editor = new PdfFileEditor();
            // Parameters: input file, output file, columns (x), rows (y)
            editor.MakeNUp(inputPdf, nupPdf, 2, 2);

            // ------------------------------------------------------------
            // Step 2: Convert the N‑up PDF into a booklet.
            // ------------------------------------------------------------
            // The MakeBooklet method reorders pages so that when the PDF
            // is printed double‑sided and folded, the pages appear in the
            // correct order.
            editor.MakeBooklet(nupPdf, bookletPdf);

            Console.WriteLine($"Booklet PDF created successfully: {bookletPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
        finally
        {
            // Clean up the intermediate N‑up file if it exists
            if (File.Exists(nupPdf))
            {
                try { File.Delete(nupPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}