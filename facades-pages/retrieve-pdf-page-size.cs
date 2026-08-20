using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file.
            editor.BindPdf(pdfPath);

            // Pages are 1‑based; retrieve size of page 2.
            PageSize size = editor.GetPageSize(2);

            // Store dimensions.
            float width = size.Width;
            float height = size.Height;

            // Log the dimensions.
            Console.WriteLine($"Page 2 size: {width} x {height}");
        }
    }
}