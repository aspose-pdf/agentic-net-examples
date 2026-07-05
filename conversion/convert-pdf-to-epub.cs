using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Default EPUB save options
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Convert and save as EPUB
            pdfDoc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputEpub}");
    }
}