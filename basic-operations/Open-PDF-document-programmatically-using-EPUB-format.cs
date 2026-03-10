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
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Load the EPUB file using the appropriate load options.
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Document implements IDisposable – wrap it in a using block.
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Save the loaded document as PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully opened and saved as PDF: {pdfPath}");
    }
}