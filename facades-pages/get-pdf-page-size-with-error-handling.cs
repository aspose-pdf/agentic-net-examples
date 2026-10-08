using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor provides GetPageSize; it does not implement IDisposable
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Define page numbers to query (including invalid ones)
        int[] pagesToCheck = { 1, 2, 10, 0, -1 };

        foreach (int pageNum in pagesToCheck)
        {
            try
            {
                // GetPageSize returns Aspose.Pdf.PageSize, not System.Drawing.SizeF
                PageSize pageSize = editor.GetPageSize(pageNum);
                Console.WriteLine($"Page {pageNum}: Width = {pageSize.Width} pt, Height = {pageSize.Height} pt");
            }
            catch (ArgumentOutOfRangeException)
            {
                // Handle non‑existent page numbers gracefully
                Console.WriteLine($"Page {pageNum} does not exist (out of range).");
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"Error retrieving size for page {pageNum}: {ex.Message}");
            }
        }

        // Release resources held by the editor
        editor.Close();
    }
}
