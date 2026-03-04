using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input EPUB file (to be opened as a PDF document)
        const string epubPath = "input.epub";
        // Output PDF file after loading the EPUB
        const string pdfPath = "output.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Create load options for EPUB format
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Open the EPUB file using the load options; the resulting Document is a PDF representation
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Save the document as a PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully opened and saved as PDF: '{pdfPath}'");
    }
}