using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file with explicit load options
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Set a custom page size (example: 6 x 9 inches, 1 inch = 72 points)
            doc.PageInfo.Width  = 6 * 72; // 432 points
            doc.PageInfo.Height = 9 * 72; // 648 points

            // Save the document as PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF with custom page size: {pdfPath}");
    }
}