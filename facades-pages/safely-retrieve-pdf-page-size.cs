using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the editor.
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Page numbers to query – includes a number that may be out of range.
        int[] pagesToCheck = { 1, 2, 10 };

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
                // Gracefully handle requests for non‑existent pages.
                Console.WriteLine($"Unable to get size for page {pageNum}: {ex.Message}");
            }
        }

        // PdfPageEditor does not require explicit disposal.
    }
}