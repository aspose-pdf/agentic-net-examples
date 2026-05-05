using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "destination.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf, Document or Close).
        Document pdfDoc = new Document(pdfPath);

        // Example usage: output the number of pages in the loaded PDF.
        Console.WriteLine($"Loaded PDF has {pdfDoc.Pages.Count} pages.");

        // No explicit Close() is required. Document implements IDisposable, so you may wrap it in a using block if desired.
        // using (var pdfDoc = new Document(pdfPath)) { /* work with pdfDoc */ }
    }
}
