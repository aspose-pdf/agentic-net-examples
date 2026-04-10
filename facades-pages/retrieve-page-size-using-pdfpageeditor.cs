using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize type

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify the input file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfPageEditor implements IDisposable – use a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(pdfPath);

            // Pages are 1‑based; retrieve size of page 2
            PageSize size = editor.GetPageSize(2);

            // Store dimensions for later use
            float width = size.Width;
            float height = size.Height;

            // Log the retrieved dimensions
            Console.WriteLine($"Page 2 size: {width} x {height}");
        }
    }
}