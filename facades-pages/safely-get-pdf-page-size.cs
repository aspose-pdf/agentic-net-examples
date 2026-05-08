using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor (lifecycle: create) and bind the PDF (load)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Define page numbers to query – include a number that may be out of range
        int[] pagesToCheck = { 1, 2, 10 };

        foreach (int pageNum in pagesToCheck)
        {
            try
            {
                // GetPageSize may throw if the page does not exist. Use 'var' to avoid
                // a direct reference to the PageSize type, which can differ between
                // Aspose.Pdf versions.
                var size = editor.GetPageSize(pageNum);
                Console.WriteLine($"Page {pageNum}: Width = {size.Width}, Height = {size.Height}");
            }
            catch (Exception ex)
            {
                // Gracefully handle requests for non‑existent pages
                Console.WriteLine($"Unable to retrieve size for page {pageNum}: {ex.Message}");
            }
        }

        // No saving is required; we only read page sizes.
    }
}
