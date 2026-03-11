using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source EPUB file that contains an embedded PDF.
        const string epubPath = "input.epub";

        // Temporary PDF file that will hold the converted content.
        const string tempPdfPath = "temp_converted.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert the EPUB to a PDF document.
        //    Document constructor with EpubLoadOptions loads the EPUB and
        //    creates a PDF representation in memory.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Save the PDF to a temporary file so that PdfFileInfo can work
            // with a file path (PdfFileInfo does not accept a Document object).
            pdfDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Retrieve page properties using PdfFileInfo (a Facade class).
        //    PdfFileInfo provides methods such as GetPageWidth, GetPageHeight,
        //    GetPageRotation, GetPageXOffset, and GetPageYOffset.
        // -----------------------------------------------------------------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
        {
            int pageCount = pdfInfo.NumberOfPages;
            Console.WriteLine($"Number of pages: {pageCount}");

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                double width  = pdfInfo.GetPageWidth(pageNumber);
                double height = pdfInfo.GetPageHeight(pageNumber);
                int    rotation = pdfInfo.GetPageRotation(pageNumber);
                double xOffset = pdfInfo.GetPageXOffset(pageNumber);
                double yOffset = pdfInfo.GetPageYOffset(pageNumber);

                Console.WriteLine(
                    $"Page {pageNumber}: Width={width}, Height={height}, Rotation={rotation}, XOffset={xOffset}, YOffset={yOffset}");
            }
        }

        // -----------------------------------------------------------------
        // 3. Clean up the temporary PDF file.
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }
    }
}