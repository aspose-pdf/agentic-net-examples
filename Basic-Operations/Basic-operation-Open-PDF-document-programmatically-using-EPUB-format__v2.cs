using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, EpubLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input EPUB file (to be opened as a PDF document)
        const string epubPath   = "input.epub";
        // Output PDF file
        const string pdfPath    = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        try
        {
            // Load the EPUB file using EpubLoadOptions.
            // The constructor without parameters uses default page size (A4, 300 dpi).
            EpubLoadOptions loadOptions = new EpubLoadOptions();

            // Document implements IDisposable, so wrap it in a using block.
            using (Document doc = new Document(epubPath, loadOptions))
            {
                // At this point the EPUB content is represented as a PDF document.
                // You can access standard PDF metadata if needed, e.g.:
                Console.WriteLine($"Title : {doc.Info.Title}");
                Console.WriteLine($"Author: {doc.Info.Author}");

                // Save the document as PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"EPUB successfully opened and saved as PDF: '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}