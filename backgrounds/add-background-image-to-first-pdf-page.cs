using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string backgroundImagePath = "background.jpg";
        const string outputPdfPath       = "output.pdf";

        // Verify the background image exists
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (page index will be 1)
            Page page = doc.Pages.Add();

            // Create an ImageStamp that will act as a background image
            ImageStamp bgStamp = new ImageStamp(backgroundImagePath)
            {
                // Place the image behind any existing content
                Background = true,

                // Scale the image to fill the entire page
                Width  = page.PageInfo.Width,
                Height = page.PageInfo.Height,

                // Center the image (required when setting explicit size)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the background stamp to the first page
            page.AddStamp(bgStamp);

            // Save the PDF – Document.Save(string) writes PDF regardless of extension
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background image saved to '{outputPdfPath}'.");
    }
}
