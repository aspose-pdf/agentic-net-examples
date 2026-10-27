using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PageDeletionAuditor
{
    // Deletes the specified pages from a PDF and logs the operation.
    public void DeletePages(string sourcePdfPath, string outputPdfPath, int[] pagesToDelete)
    {
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Log how many pages are requested for deletion.
        Console.WriteLine($"Requesting deletion of {pagesToDelete.Length} page(s) from \"{Path.GetFileName(sourcePdfPath)}\"...");

        // Load the source document to determine the original page count.
        int originalPageCount;
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            originalPageCount = srcDoc.Pages.Count;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the deletion. The correct overload order is (source, pagesToDelete, output).
        editor.Delete(sourcePdfPath, pagesToDelete, outputPdfPath);

        // Load the resulting document to determine the new page count.
        int resultingPageCount;
        using (Document resultDoc = new Document(outputPdfPath))
        {
            resultingPageCount = resultDoc.Pages.Count;
        }

        int actuallyDeleted = originalPageCount - resultingPageCount;
        // Log the result of the operation.
        Console.WriteLine($"Deleted {actuallyDeleted} page(s) (requested: {pagesToDelete.Length}). Output saved to \"{Path.GetFileName(outputPdfPath)}\".");
        Console.WriteLine($"Resulting document contains {resultingPageCount} page(s).");
    }
}

class Program
{
    static void Main()
    {
        // Example usage:
        const string inputPath = "input.pdf";
        const string outputPath = "output_deleted.pdf";

        // Define the pages to delete (1‑based indexing).
        int[] pagesToRemove = new int[] { 2, 4, 5 };

        PageDeletionAuditor auditor = new PageDeletionAuditor();
        auditor.DeletePages(inputPath, outputPath, pagesToRemove);
    }
}
