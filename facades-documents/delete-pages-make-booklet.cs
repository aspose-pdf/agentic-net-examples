using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor, PageSize enum resides in Aspose.Pdf
using Aspose.Pdf;          // for PageSize enum

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Pages to remove (1‑based indexing). Example: remove pages 2 and 5.
        int[] pagesToDelete = new int[] { 2, 5 };

        // Temporary file that will hold the PDF after deletion
        string tempPdf = Path.GetTempFileName();

        // Final booklet output file
        const string bookletPdf = "booklet_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Delete unwanted pages using PdfFileEditor.TryDelete
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            bool deleteSuccess = editor.TryDelete(inputPdf, pagesToDelete, tempPdf);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Failed to delete pages from the source PDF.");
                return;
            }

            // ------------------------------------------------------------
            // Step 2: Create a booklet from the intermediate PDF
            // ------------------------------------------------------------
            // You can choose a page size for the booklet; here we use A4.
            bool bookletSuccess = editor.TryMakeBooklet(tempPdf, bookletPdf, PageSize.A4);
            if (!bookletSuccess)
            {
                Console.Error.WriteLine("Failed to create booklet PDF.");
                return;
            }

            Console.WriteLine($"Booklet created successfully: {bookletPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file
            try
            {
                if (File.Exists(tempPdf))
                {
                    File.Delete(tempPdf);
                }
            }
            catch
            {
                // Ignored – best‑effort cleanup
            }
        }
    }
}