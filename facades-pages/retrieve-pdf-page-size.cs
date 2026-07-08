using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfPageEditor implements IDisposable – wrap it in a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(pdfPath);

            // Pages are 1‑based; retrieve the size of page 2
            PageSize size = editor.GetPageSize(2);

            // Store width and height for later use or logging
            float width = size.Width;
            float height = size.Height;

            // Output the dimensions
            Console.WriteLine($"Page 2 size: {width} x {height}");
        }
    }
}