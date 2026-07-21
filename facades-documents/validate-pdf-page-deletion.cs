using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, pages to delete, and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToDelete = new int[] { 2, 3 }; // delete pages 2 and 3 (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get page count before deletion using PdfPageEditor (Facades API)
        PdfPageEditor pageEditorBefore = new PdfPageEditor();
        pageEditorBefore.BindPdf(inputPath);
        int beforeCount = pageEditorBefore.GetPages();
        pageEditorBefore.Close(); // optional, releases resources

        // Perform deletion using PdfFileEditor
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.Delete(inputPath, pagesToDelete, outputPath);
        // No return value; operation throws on failure

        // Get page count after deletion
        PdfPageEditor pageEditorAfter = new PdfPageEditor();
        pageEditorAfter.BindPdf(outputPath);
        int afterCount = pageEditorAfter.GetPages();
        pageEditorAfter.Close();

        // Validate that the page count decreased by the number of deleted pages
        int expectedCount = beforeCount - pagesToDelete.Length;
        bool isValid = afterCount == expectedCount;

        Console.WriteLine($"Pages before delete: {beforeCount}");
        Console.WriteLine($"Pages after delete : {afterCount}");
        Console.WriteLine(isValid
            ? "Delete operation succeeded: page count reduced as expected."
            : $"Delete operation failed: expected {expectedCount} pages, but got {afterCount}.");
    }
}