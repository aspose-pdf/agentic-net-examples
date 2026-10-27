using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PageSize type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does not implement IDisposable, so no using block is needed
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Load the PDF document into the editor
        pageEditor.BindPdf(inputPath);

        // Page numbers are 1‑based; retrieve size of page 2 using Aspose.Pdf.PageSize
        PageSize pageSize = pageEditor.GetPageSize(2);

        // Log the dimensions (width and height in points)
        Console.WriteLine($"Page 2 size: Width = {pageSize.Width} pt, Height = {pageSize.Height} pt");

        // No need to save changes because we only read the size
    }
}