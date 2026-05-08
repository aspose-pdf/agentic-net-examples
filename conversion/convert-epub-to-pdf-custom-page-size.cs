using System;
using System.Drawing; // for SizeF
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string epubPath   = "input.epub";
        const string pdfPath    = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Define a custom page size (width x height) in points.
        // Example: 600 x 800 points (≈8.33 x 11.11 inches at 72 DPI)
        SizeF customPageSize = new SizeF(600f, 800f);

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