using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "background.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF has fewer than three pages.");
                return;
            }

            // Retrieve page three (Aspose.Pdf uses 1‑based indexing)
            Page pageThree = doc.Pages[3];

            // Create an ImageStamp that will be used as a background image
            ImageStamp bgStamp = new ImageStamp(imagePath)
            {
                // Position the stamp to cover the whole page
                Width = pageThree.PageInfo.Width,
                Height = pageThree.PageInfo.Height,
                // Place the stamp behind existing page content
                Background = true,
                // Align the stamp to the centre of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Add the background stamp to page three
            pageThree.AddStamp(bgStamp);

            // Save the modified PDF; other pages remain unchanged
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background image added to page 3 and saved as '{outputPath}'.");
    }
}
