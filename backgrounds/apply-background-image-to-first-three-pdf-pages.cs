using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF (must have at least 5 pages)
        const string outputPath = "output.pdf";         // result PDF
        const string bgImagePath = "background.jpg";    // background image to apply

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine how many pages we can process (first three or fewer if the doc is shorter)
            int pagesToProcess = Math.Min(3, doc.Pages.Count);

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= pagesToProcess; i++)
            {
                // Create an ImageStamp for the background image
                ImageStamp bgStamp = new ImageStamp(bgImagePath)
                {
                    Background = true,                                 // render as background
                    HorizontalAlignment = HorizontalAlignment.Center,   // optional alignment
                    VerticalAlignment   = VerticalAlignment.Center,    // optional alignment
                    Opacity = 0.5                                      // optional transparency
                };

                // Add the stamp to the current page (must call AddStamp on each Page instance)
                doc.Pages[i].AddStamp(bgStamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background image applied to first three pages. Saved as '{outputPath}'.");
    }
}