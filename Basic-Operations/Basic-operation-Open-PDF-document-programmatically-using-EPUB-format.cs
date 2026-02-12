using System;
using System.IO;
using Aspose.Pdf;   // Provides Document, EpubLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input EPUB file (to be opened as a document)
        const string epubPath = "sample.epub";

        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the source EPUB exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at '{epubPath}'.");
            return;
        }

        // Load the EPUB using EpubLoadOptions (default options)
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // The Document constructor with (string, LoadOptions) loads the EPUB and creates a PDF document in memory
        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            // Save the in‑memory PDF to a file
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Successfully converted '{epubPath}' to PDF at '{pdfPath}'.");
    }
}