using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input image files – ensure they exist in the working directory
        const string backgroundPath = "background.tif";
        const string iconPath       = "icon.png";
        const string outputPath     = "overlay.pdf";

        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }
        if (!File.Exists(iconPath))
        {
            Console.Error.WriteLine($"Icon image not found: {iconPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Add the background image using ImageStamp (absolute positioning)
            // -----------------------------------------------------------------
            ImageStamp backgroundStamp = new ImageStamp(backgroundPath)
            {
                // Set the desired width; height will be scaled proportionally
                Width   = 500,
                // Position at the top‑left corner of the page
                XIndent = 0,
                YIndent = 0,
                // Ensure the image is not treated as a watermark (no background fill)
                Background = false
            };
            page.AddStamp(backgroundStamp);

            // -----------------------------------------------------------------
            // Add the overlay icon image on top of the background
            // -----------------------------------------------------------------
            ImageStamp iconStamp = new ImageStamp(iconPath)
            {
                // Example positioning – adjust as needed
                XIndent = 50,
                YIndent = 50,
                // Optional: set a fixed size for the icon
                // Width  = 100,
                // Height = 100,
                Background = false
            };
            page.AddStamp(iconStamp);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created and saved to '{outputPath}'.");
    }
}
