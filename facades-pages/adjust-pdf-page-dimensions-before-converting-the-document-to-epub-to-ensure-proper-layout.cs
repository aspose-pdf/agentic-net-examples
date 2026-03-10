using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // original PDF
        const string resizedPdf = "resized.pdf"; // temporary PDF with new page size
        const string outputEpub = "output.epub"; // final EPUB file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Adjust page dimensions using PdfFileEditor (Facades API)
        // -----------------------------------------------------------------
        // Example: set all pages to 600 x 800 points (1 point = 1/72 inch)
        // Passing null for the pages array processes every page.
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(inputPdf, resizedPdf, null, 600, 800);

        // -----------------------------------------------------------------
        // Load the resized PDF and convert it to EPUB
        // -----------------------------------------------------------------
        using (Document doc = new Document(resizedPdf))
        {
            // Configure EPUB save options – use the Flow recognition mode
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save as EPUB; explicit SaveOptions are required for non‑PDF formats
            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"EPUB created successfully at '{outputEpub}'.");
    }
}