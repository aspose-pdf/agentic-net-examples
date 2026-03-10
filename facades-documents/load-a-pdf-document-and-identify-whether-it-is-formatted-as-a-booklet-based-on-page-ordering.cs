using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original PDF into a stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Create a memory stream to hold the booklet version
            using (MemoryStream bookletStream = new MemoryStream())
            {
                // Use PdfFileEditor to generate a booklet version
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputStream, bookletStream);

                if (!success)
                {
                    Console.WriteLine("Failed to create booklet version.");
                    return;
                }

                // Reset streams for reading
                inputStream.Position = 0;
                bookletStream.Position = 0;

                // Retrieve page counts for original and booklet PDFs
                PdfFileInfo originalInfo = new PdfFileInfo(inputStream);
                PdfFileInfo bookletInfo = new PdfFileInfo(bookletStream);

                int originalPages = originalInfo.NumberOfPages;
                int bookletPages = bookletInfo.NumberOfPages;

                // Simple heuristic: if page counts are equal and the original
                // ordering already matches a booklet layout, we consider it a booklet.
                // A common booklet layout requires the total page count to be a multiple of 4.
                bool isMultipleOfFour = originalPages % 4 == 0;

                // Compare the first and last page numbers to detect typical imposition:
                // In a booklet the first page often corresponds to the last logical page.
                bool firstIsLast = originalPages > 1 && originalInfo.GetPageWidth(1) == originalInfo.GetPageWidth(originalPages);

                bool isBooklet = isMultipleOfFour && firstIsLast && originalPages == bookletPages;

                Console.WriteLine($"Total pages: {originalPages}");
                Console.WriteLine($"Is multiple of 4: {isMultipleOfFour}");
                Console.WriteLine($"First page size equals last page size: {firstIsLast}");
                Console.WriteLine($"Detected as booklet: {isBooklet}");
            }
        }
    }
}