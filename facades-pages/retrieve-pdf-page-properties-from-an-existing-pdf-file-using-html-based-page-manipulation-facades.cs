using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Retrieve the total number of pages.
            int pageCount = pdfInfo.NumberOfPages;
            Console.WriteLine($"Total pages: {pageCount}");

            // Iterate through each page (1‑based indexing).
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Get page dimensions (points; 1 inch = 72 points).
                double width  = pdfInfo.GetPageWidth(pageNumber);
                double height = pdfInfo.GetPageHeight(pageNumber);

                // Get page rotation (0, 90, 180, 270 degrees).
                int rotation = pdfInfo.GetPageRotation(pageNumber);

                // Get page display offsets (if any).
                double xOffset = pdfInfo.GetPageXOffset(pageNumber);
                double yOffset = pdfInfo.GetPageYOffset(pageNumber);

                Console.WriteLine($"Page {pageNumber}:");
                Console.WriteLine($"  Width   = {width} pt");
                Console.WriteLine($"  Height  = {height} pt");
                Console.WriteLine($"  Rotation= {rotation}°");
                Console.WriteLine($"  XOffset = {xOffset} pt");
                Console.WriteLine($"  YOffset = {yOffset} pt");
            }
        }
    }
}