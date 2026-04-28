using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output EPUB file path
        const string epubPath = "output.epub";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize default EPUB save options (required for non‑PDF output)
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save the document as EPUB using the provided save options
            pdfDocument.Save(epubPath, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {epubPath}");
    }
}