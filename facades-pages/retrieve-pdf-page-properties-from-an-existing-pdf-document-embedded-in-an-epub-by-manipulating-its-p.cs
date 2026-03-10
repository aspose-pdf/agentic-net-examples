using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string outputPdfPath = "extracted.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file as a PDF document using EpubLoadOptions
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Optional: save the extracted PDF for verification
            pdfDoc.Save(outputPdfPath);

            // Use PdfFileInfo facade to retrieve page properties
            PdfFileInfo info = new PdfFileInfo(pdfDoc);

            int pageCount = info.NumberOfPages;
            Console.WriteLine($"Number of pages: {pageCount}");

            for (int i = 1; i <= pageCount; i++)
            {
                double width = info.GetPageWidth(i);
                double height = info.GetPageHeight(i);
                int rotation = info.GetPageRotation(i);
                double xOffset = info.GetPageXOffset(i);
                double yOffset = info.GetPageYOffset(i);

                Console.WriteLine($"Page {i}: Width={width}, Height={height}, Rotation={rotation}, XOffset={xOffset}, YOffset={yOffset}");
            }
        }
    }
}