using System;
using System.IO;
using System.Drawing; // SizeF for custom page dimensions
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Example custom page size: Letter (8.5 x 11 inches) at 72 DPI => 612 x 792 points
        SizeF customPageSize = new SizeF(612f, 792f);
        // EpubLoadOptions accepts a System.Drawing.SizeF for the desired page size.
        var loadOptions = new EpubLoadOptions(customPageSize);

        // Load EPUB with the specified options and convert to PDF
        using (var pdfDocument = new Document(epubPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF: {pdfPath}");
    }
}
