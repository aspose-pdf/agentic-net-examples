using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string patternPath = "pattern.png";       // background pattern image
        const string outputPath = "output.pdf";         // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(patternPath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp for the background pattern
                ImageStamp bgStamp = new ImageStamp(patternPath)
                {
                    // Place the stamp behind existing page content
                    Background = true,

                    // Set opacity to 10 % (0.1)
                    Opacity = 0.1,

                    // Stretch the image to cover the whole page
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height,

                    // Center the image (optional, since we stretch it)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to the current page
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPath}'.");
    }
}