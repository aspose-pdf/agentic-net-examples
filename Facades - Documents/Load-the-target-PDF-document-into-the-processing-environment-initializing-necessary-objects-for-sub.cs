using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfViewer implements IDisposable, so we use a using block for deterministic cleanup.
        using (PdfViewer viewer = new PdfViewer())
        {
            // Load the PDF into the viewer.
            viewer.BindPdf(inputPath);

            // PdfViewer exposes the number of pages via the PageCount property.
            int pageCount = viewer.PageCount;
            Console.WriteLine($"Loaded PDF with {pageCount} pages.");
        }
    }
}