using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (source) and output EPUB paths
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, set its metadata, and convert to EPUB
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Set PDF metadata (Title, Author, Subject, Keywords) via PdfFileInfo
            // -----------------------------------------------------------------
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfDoc);
            pdfInfo.Title    = "Sample EPUB Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of metadata transfer";
            pdfInfo.Keywords = "Aspose, EPUB, metadata";

            // -----------------------------------------------------------------
            // Prepare EPUB save options – the Title property sets the EPUB title
            // -----------------------------------------------------------------
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = pdfInfo.Title   // synchronize EPUB title with PDF title
            };

            // -----------------------------------------------------------------
            // Save the document as EPUB using explicit save options
            // -----------------------------------------------------------------
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"EPUB file created with metadata at '{outputEpubPath}'.");
    }
}