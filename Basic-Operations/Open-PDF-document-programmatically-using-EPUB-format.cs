using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";

        // Verify the EPUB file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Load the EPUB file using EpubLoadOptions (converts to PDF internally)
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Save the resulting PDF document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully opened and saved as PDF: {pdfPath}");
    }
}