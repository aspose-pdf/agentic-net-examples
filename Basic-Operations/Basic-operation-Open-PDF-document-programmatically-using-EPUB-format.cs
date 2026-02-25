using System;
using System.IO;
using Aspose.Pdf; // Document, EpubLoadOptions

class Program
{
    static void Main()
    {
        // Path to the source EPUB file (will be opened as a PDF document)
        const string epubPath = "input.epub";

        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Load the EPUB file using EpubLoadOptions and treat it as a PDF document
        EpubLoadOptions loadOptions = new EpubLoadOptions(); // default options
        using (Document pdfDocument = new Document(epubPath, loadOptions))
        {
            // Save the loaded document as a PDF file
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"EPUB file '{epubPath}' opened and saved as PDF to '{pdfPath}'.");
    }
}