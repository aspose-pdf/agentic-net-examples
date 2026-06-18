using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input files
        const string originalPdf = "original.pdf";      // PDF from which pages will be removed
        const string secondPdf   = "second.pdf";        // PDF to concatenate after deletion
        // Temporary file after page deletion
        const string editedPdf   = "edited_temp.pdf";
        // Final output after concatenation
        const string finalPdf    = "final_output.pdf";

        // Pages to delete (1‑based indexing). Example: delete pages 2 and 3.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify source files exist
        if (!File.Exists(originalPdf))
        {
            Console.Error.WriteLine($"File not found: {originalPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            // Create the PdfFileEditor facade (does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Delete specified pages from the original PDF and save to a temporary file
            bool deleteSuccess = editor.Delete(originalPdf, pagesToDelete, editedPdf);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Page deletion failed.");
                return;
            }

            // Concatenate the edited PDF with the second PDF into the final output file
            bool concatSuccess = editor.Concatenate(editedPdf, secondPdf, finalPdf);
            if (!concatSuccess)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            Console.WriteLine($"Successfully created '{finalPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}