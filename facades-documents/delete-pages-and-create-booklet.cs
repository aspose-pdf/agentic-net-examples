using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, pages to remove, and final booklet output paths
        const string inputPdf      = "input.pdf";
        const string tempPdf       = "temp_deleted.pdf";   // intermediate file after deletion
        const string bookletPdf    = "booklet_output.pdf";

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 5, 7 };

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // 1) Delete unwanted pages and save to a temporary PDF
        bool deleteResult = editor.Delete(inputPdf, pagesToDelete, tempPdf);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Failed to delete pages.");
            return;
        }

        // 2) Create a booklet from the temporary PDF and save the final result
        bool bookletResult = editor.MakeBooklet(tempPdf, bookletPdf);
        if (!bookletResult)
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
            // If deletion fails, ignore – the file may be in use or protected.
        }

        Console.WriteLine($"Booklet created successfully: {bookletPdf}");
    }
}