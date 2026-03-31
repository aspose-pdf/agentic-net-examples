using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const int pageNumber = 5; // Change to the page you want to query

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using‑statement to ensure resources are released
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            try
            {
                // GetPageSize may throw if the page does not exist
                PageSize pageSize = editor.GetPageSize(pageNumber);
                Console.WriteLine($"Page {pageNumber} size: {pageSize.Width} x {pageSize.Height}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get size for page {pageNumber}: {ex.Message}");
            }
        }
    }
}
