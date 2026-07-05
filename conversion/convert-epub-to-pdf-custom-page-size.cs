using System;
using System.IO;
using System.Drawing;          // SizeF for custom page dimensions
using Aspose.Pdf;              // Core Aspose.Pdf API

class Program
{
    static void Main()
    {
        // Input EPUB file and desired PDF output
        const string epubPath   = "input.epub";
        const string pdfPath    = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Define a custom page size (e.g., 8.5 x 11 inches at 72 DPI)
        // Width = 8.5 * 72 = 612, Height = 11 * 72 = 792
        SizeF customPageSize = new SizeF(612f, 792f);

        // Initialize load options with the custom page size
        EpubLoadOptions loadOptions = new EpubLoadOptions(customPageSize);

        // Load the EPUB and convert to PDF using a using block for deterministic disposal
        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB converted to PDF successfully: {pdfPath}");
    }
}