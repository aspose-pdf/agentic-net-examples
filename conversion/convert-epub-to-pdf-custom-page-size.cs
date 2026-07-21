using System;
using System.IO;
using System.Drawing; // for SizeF (page dimensions)
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Define a custom page size (width x height) in points.
        // Example: 8.5 x 11 inches at 300 DPI => 2550 x 3300 points.
        SizeF customPageSize = new SizeF(2550f, 3300f);

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