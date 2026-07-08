using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        const string inputPath = "sample.pdf";

        // Verify that the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor and bind the PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Page numbers to query – includes a valid and an invalid number
        int[] pagesToCheck = { 1, 2, 10 };

        foreach (int pageNum in pagesToCheck)
        {
            try
            {
                // GetPageSize throws if the page number does not exist.
                // Use 'var' so the exact return type (PageSizeInfo) does not need to be referenced explicitly.
                var size = editor.GetPageSize(pageNum);
                Console.WriteLine($"Page {pageNum}: Width = {size.Width}, Height = {size.Height}");
            }
            catch (Exception ex)
            {
                // Handle the case where the requested page is out of range
                Console.WriteLine($"Unable to get size for page {pageNum}: {ex.Message}");
            }
        }

        // PdfPageEditor does not implement IDisposable; no explicit disposal needed
    }
}
