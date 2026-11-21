using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "output.pdf";

        // Verify required files exist.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a reusable ImageStamp.
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Pages are 1‑based. Loop through pages 5‑10, respecting the actual page count.
            int startPage = 5;
            int endPage   = 10;
            int maxPage   = pdfDocument.Pages.Count;

            for (int i = startPage; i <= endPage && i <= maxPage; i++)
            {
                Page page = pdfDocument.Pages[i];
                page.AddStamp(imgStamp); // Apply the stamp to the current page.
            }

            // Save while the document is still alive.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied to pages 5‑10 and saved to '{outputPath}'.");
    }
}