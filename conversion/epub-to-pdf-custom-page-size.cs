using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Custom page size (Letter: 612 x 792 points)
        SizeF customSize = new SizeF(612f, 792f);
        EpubLoadOptions loadOptions = new EpubLoadOptions(customSize);

        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB converted to PDF: {pdfPath}");
    }
}