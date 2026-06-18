using System;
using System.Drawing;          // for SizeF
using System.IO;
using Aspose.Pdf;               // core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input EPUB file path
        const string epubPath = "input.epub";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Define a custom page size (e.g., 8.5 x 11 inches at 72 DPI)
        // Width = 8.5 * 72 = 612, Height = 11 * 72 = 792
        SizeF customPageSize = new SizeF(612f, 792f);

        // Create load options with the custom page size
        EpubLoadOptions loadOptions = new EpubLoadOptions(customPageSize);

        // Load the EPUB and convert it to PDF
        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB converted to PDF successfully: {pdfPath}");
    }
}