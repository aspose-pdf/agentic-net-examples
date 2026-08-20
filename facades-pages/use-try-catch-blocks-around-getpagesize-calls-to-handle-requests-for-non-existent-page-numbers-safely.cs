using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify that the source PDF exists.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor instance and bind the PDF file.
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Define the page numbers we want to query.
        // Include a number that may be out of range to demonstrate error handling.
        int[] pagesToCheck = { 1, 2, 100 };

        foreach (int pageNum in pagesToCheck)
        {
            try
            {
                // GetPageSize throws if the page number does not exist.
                PageSize size = editor.GetPageSize(pageNum);
                Console.WriteLine($"Page {pageNum}: Width = {size.Width}, Height = {size.Height}");
            }
            catch (Exception ex)
            {
                // Handle non‑existent page numbers (or other errors) gracefully.
                Console.WriteLine($"Unable to retrieve size for page {pageNum}: {ex.Message}");
            }
        }

        // No saving is required; we only read page dimensions.
    }
}
