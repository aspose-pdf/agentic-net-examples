using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions classes are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputEpub = "output.epub";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create default EPUB save options (no custom settings)
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save the document as EPUB using the explicit options object
            pdfDoc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputEpub}");
    }
}