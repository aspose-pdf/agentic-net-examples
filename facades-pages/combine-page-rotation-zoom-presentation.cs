using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "presentation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Number of pages (1‑based indexing is handled internally by the Pages collection)
        int pageCount = pdfDocument.Pages.Count;

        // Define a zoom factor (e.g., 1.2 = 120% of original size)
        const double zoomFactor = 1.2;

        for (int i = 1; i <= pageCount; i++)
        {
            // Get the current page
            Page page = pdfDocument.Pages[i];

            // Rotate the page 90 degrees clockwise
            page.Rotate = Rotation.on90;

            // Retrieve the original page dimensions
            double originalWidth = page.PageInfo.Width;
            double originalHeight = page.PageInfo.Height;

            // Apply zoom by adjusting the page size. This scales the page content.
            page.PageInfo.Width = originalWidth * zoomFactor;
            page.PageInfo.Height = originalHeight * zoomFactor;
        }

        // Save the transformed PDF to a new file
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Presentation PDF with rotation and zoom saved to '{outputPath}'.");
    }
}
