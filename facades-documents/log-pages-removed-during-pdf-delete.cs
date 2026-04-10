using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PageDeletionAuditor
{
    // Logs the number of pages removed during each delete operation.
    // Uses Aspose.Pdf.Facades.PdfFileEditor for the deletion.
    static void Main()
    {
        // Example usage:
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        // Pages to delete (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToDelete = new int[] { 2, 3, 5 };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Get original page count.
            int originalPageCount;
            using (Document srcDoc = new Document(inputPdf))
            {
                originalPageCount = srcDoc.Pages.Count;
            }

            // Perform deletion using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            bool deleteResult = editor.TryDelete(inputPdf, pagesToDelete, outputPdf);

            if (!deleteResult)
            {
                Console.Error.WriteLine("Delete operation failed.");
                return;
            }

            // Get new page count after deletion.
            int newPageCount;
            using (Document outDoc = new Document(outputPdf))
            {
                newPageCount = outDoc.Pages.Count;
            }

            // Calculate how many pages were actually removed.
            int pagesRemoved = originalPageCount - newPageCount;
            Console.WriteLine($"Delete operation completed successfully.");
            Console.WriteLine($"Original page count: {originalPageCount}");
            Console.WriteLine($"Requested pages to delete: {pagesToDelete.Length}");
            Console.WriteLine($"Actual pages removed: {pagesRemoved}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}