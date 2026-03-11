using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        // Desired page dimensions in points (1 inch = 72 points).
        // Example: A4 size = 595 x 842 points.
        double newWidth = 595;
        double newHeight = 842;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF.
        Document pdfDocument = new Document(inputPath);

        // Resize each page.
        foreach (Page page in pdfDocument.Pages)
        {
            // Set the new page dimensions.
            page.PageInfo.Width = newWidth;
            page.PageInfo.Height = newHeight;

            // Optional: keep the original content scaling and centre it.
            // The content is automatically scaled to fit the new size when the
            // page dimensions are changed. If you need custom placement you can
            // use page.PageInfo.Margin, but for a simple centre‑align the default
            // behaviour is sufficient.
        }

        // Save the resized document.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
