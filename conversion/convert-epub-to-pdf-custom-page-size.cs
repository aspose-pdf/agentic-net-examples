using System;
using System.IO;
using System.Drawing;          // Required for SizeF
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";

        // Verify the source EPUB file exists.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Define a custom page size (width x height) in points.
        // Example: US Letter size 8.5" x 11" at 72 DPI => 612 x 792 points.
        SizeF customPageSize = new SizeF(612f, 792f);

        // Initialize load options with the custom page size.
        EpubLoadOptions loadOptions = new EpubLoadOptions(customPageSize);

        // Load the EPUB using the options and convert it to PDF.
        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF: {pdfPath}");
    }
}