using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input files
        const string sourcePdf = "source.pdf";          // PDF from which pages will be removed
        const string secondPdf = "second.pdf";          // PDF to concatenate after deletion
        const string tempPdf   = "temp_deleted.pdf";    // Intermediate file after page deletion
        const string outputPdf = "final_merged.pdf";    // Resulting merged PDF

        // Pages to delete (1‑based indexing). Example: delete pages 2 and 4.
        int[] pagesToDelete = new int[] { 2, 4 };

        // Verify that input files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"Second file not found: {secondPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Delete specified pages from the source PDF.
            // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Delete pages and write the result to a temporary file.
            bool deleteSuccess = editor.Delete(sourcePdf, pagesToDelete, tempPdf);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Page deletion failed.");
                return;
            }

            // ------------------------------------------------------------
            // Step 2: Concatenate the edited PDF with the second PDF.
            // ------------------------------------------------------------
            bool concatSuccess = editor.Concatenate(tempPdf, secondPdf, outputPdf);
            if (!concatSuccess)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            // Optional: clean up the intermediate file.
            if (File.Exists(tempPdf))
            {
                File.Delete(tempPdf);
            }

            Console.WriteLine($"Successfully created merged PDF: '{outputPdf}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}