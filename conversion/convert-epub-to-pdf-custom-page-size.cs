using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath   = "input.epub";
        const string pdfPath    = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB using EpubLoadOptions
        EpubLoadOptions loadOptions = new EpubLoadOptions();
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Set a custom page size (width and height are in points)
            // Example: 600 x 800 points
            doc.PageInfo.Width  = 600;
            doc.PageInfo.Height = 800;

            // Save the document as PDF (extension .pdf forces PDF output)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB converted to PDF with custom page size: {pdfPath}");
    }
}