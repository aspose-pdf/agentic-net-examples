using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Document must be disposed deterministically
        using (Document doc = new Document())
        {
            // Add the first page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create an ImageStamp for the background image
            ImageStamp bgStamp = new ImageStamp(imagePath)
            {
                // Place the image behind other page content
                Background = true,
                // Stretch the image to fill the entire page
                Width = page.PageInfo.Width,
                Height = page.PageInfo.Height,
                // Center alignment (optional, as size matches page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the background stamp to the page
            page.AddStamp(bgStamp);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
