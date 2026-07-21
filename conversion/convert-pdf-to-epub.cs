using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace contains Document and EpubSaveOptions

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the target EPUB file
        const string pdfPath  = "input.pdf";
        const string epubPath = "output.epub";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: create & load)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize default EPUB save options (no custom settings required)
            EpubSaveOptions saveOptions = new EpubSaveOptions();

            // Save the document as EPUB using the options (lifecycle: save)
            pdfDocument.Save(epubPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{epubPath}'");
    }
}