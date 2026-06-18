using System;
using System.IO;
using Aspose.Pdf;   // All save options, including EpubSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputEpub = "output.epub";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (Document implements IDisposable, so wrap in using)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize default EPUB save options – no custom settings required
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save the document as EPUB using the explicit save options.
            // This follows the rule: Document.Save(string, SaveOptions) for non‑PDF formats.
            pdfDocument.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputEpub}'");
    }
}