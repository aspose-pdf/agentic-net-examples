using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and retrieve the size of page 2 (pages are 1‑based)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(pdfPath);
            PageSize size = editor.GetPageSize(2);
            Console.WriteLine($"Page 2 size: {size.Width} x {size.Height}");
        }
    }
}