using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfPageEditor is a Facades class used for page‑level operations
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Bind the PDF file to the editor
        pageEditor.BindPdf(pdfPath);

        // Example 1: Get size of a valid page (pages are 1‑based)
        int validPageNumber = 1;
        try
        {
            // GetPageSize returns Aspose.Pdf.PageSize, not System.Drawing.SizeF
            PageSize size = pageEditor.GetPageSize(validPageNumber);
            Console.WriteLine($"Page {validPageNumber} size: {size.Width} x {size.Height} points");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Handles request for a page number that does not exist
            Console.WriteLine($"Page {validPageNumber} does not exist: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic fallback for any other unexpected errors
            Console.WriteLine($"Error retrieving size for page {validPageNumber}: {ex.Message}");
        }

        // Example 2: Attempt to get size of a non‑existent page
        int invalidPageNumber = 999;
        try
        {
            PageSize size = pageEditor.GetPageSize(invalidPageNumber);
            Console.WriteLine($"Page {invalidPageNumber} size: {size.Width} x {size.Height} points");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Expected path when the page number is out of range
            Console.WriteLine($"Page {invalidPageNumber} does not exist: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic fallback for any other unexpected errors
            Console.WriteLine($"Error retrieving size for page {invalidPageNumber}: {ex.Message}");
        }

        // No explicit disposal is required for PdfPageEditor; it does not implement IDisposable.
        // The bound PDF file will be released when the program exits.
    }
}
