using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document API provides page size manipulation)
        Document pdfDocument = new Document(inputPath);

        const int targetPage = 8; // page number to work with (1‑based)

        // Store the original page dimensions before any modification
        Page page = pdfDocument.Pages[targetPage];
        double originalWidth = page.PageInfo.Width;
        double originalHeight = page.PageInfo.Height;

        // Example modification: change the page size to A4 dimensions (595 x 842 points)
        // Use the overload that accepts width and height as doubles.
        page.SetPageSize(595, 842);

        // Revert the page size back to the originally stored dimensions
        page.SetPageSize(originalWidth, originalHeight);

        // Save the modified PDF to a new file
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Page {targetPage} size reverted and saved to '{outputPath}'.");
    }
}
