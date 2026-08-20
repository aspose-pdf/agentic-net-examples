using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: create and load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize default EPUB save options (default conversion settings)
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save the document as EPUB using the options (lifecycle: save)
            pdfDoc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputEpub}");
    }
}