using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, pages to remove, and final booklet output
        const string inputPdf = "input.pdf";
        const string tempPdf = "temp_deleted.pdf";
        const string bookletPdf = "booklet_output.pdf";

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Delete unwanted pages ----------
        // PdfFileEditor does NOT implement IDisposable, so we instantiate normally.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save to a temporary file.
        bool deleteSuccess = editor.Delete(inputPdf, pagesToDelete, tempPdf);
        if (!deleteSuccess)
        {
            Console.Error.WriteLine("Failed to delete pages.");
            return;
        }

        // ---------- Create booklet from the cleaned PDF ----------
        // Re‑use the same PdfFileEditor instance (or create a new one).
        bool bookletSuccess = editor.MakeBooklet(tempPdf, bookletPdf);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        // Optional: clean up the intermediate file
        try
        {
            File.Delete(tempPdf);
        }
        catch
        {
            // Ignore any errors during cleanup
        }

        Console.WriteLine($"Booklet created successfully: {bookletPdf}");
    }
}