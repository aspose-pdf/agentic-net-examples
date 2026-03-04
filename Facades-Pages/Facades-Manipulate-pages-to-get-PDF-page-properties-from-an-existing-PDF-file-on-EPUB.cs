using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string epubPath  = "output.epub"; // target EPUB

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Use PdfFileInfo (Facades) to read page‑level properties.
        // -----------------------------------------------------------------
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            int pageCount = info.NumberOfPages;
            Console.WriteLine($"Number of pages: {pageCount}");

            for (int i = 1; i <= pageCount; i++) // pages are 1‑based
            {
                double width   = info.GetPageWidth(i);
                double height  = info.GetPageHeight(i);
                int    rotation = info.GetPageRotation(i);
                double xOffset = info.GetPageXOffset(i);
                double yOffset = info.GetPageYOffset(i);

                Console.WriteLine($"Page {i}: Width={width}, Height={height}, Rotation={rotation}, XOffset={xOffset}, YOffset={yOffset}");
            }
        }

        // -----------------------------------------------------------------
        // Convert the same PDF to EPUB format.
        // Explicit SaveOptions are required; otherwise the file would be saved as PDF.
        // -----------------------------------------------------------------
        using (Document doc = new Document(pdfPath))
        {
            var epubOptions = new EpubSaveOptions
            {
                // Use the Flow recognition mode for best reflow on e‑readers.
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            doc.Save(epubPath, epubOptions);
        }

        Console.WriteLine($"PDF has been converted to EPUB: {epubPath}");
    }
}