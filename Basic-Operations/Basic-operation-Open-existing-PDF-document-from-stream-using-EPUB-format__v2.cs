using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, EpubSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF from a FileStream inside a using block for deterministic disposal.
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Document pdfDoc = new Document(pdfStream))
        {
            // Configure EPUB save options.
            // ContentRecognitionMode is a nested enum inside EpubSaveOptions.
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                // Choose the desired recognition mode (Flow, PdfFlow, or Fixed).
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB using the explicit options.
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
    }
}