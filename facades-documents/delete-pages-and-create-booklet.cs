using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the temporary file after deletion, and the final booklet PDF
        const string inputPdf      = "input.pdf";
        const string tempPdf       = "temp_deleted.pdf";
        const string bookletPdf    = "booklet_output.pdf";

        // Pages to remove (1‑based indexing). Example: remove pages 2 and 5.
        int[] pagesToDelete = new int[] { 2, 5 };

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create the PdfFileEditor instance (it does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // 1. Delete the unwanted pages, writing the result to a temporary PDF file
        bool deleteResult = editor.Delete(inputPdf, pagesToDelete, tempPdf);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Failed to delete pages.");
            return;
        }

        // 2. Create a booklet from the temporary PDF and save it to the final output file
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
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Booklet created successfully: {bookletPdf}");
    }
}