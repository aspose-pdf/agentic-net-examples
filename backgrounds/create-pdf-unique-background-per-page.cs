using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to background images (ensure these files exist)
        string[] bgImages = {
            "bg1.jpg",
            "bg2.jpg",
            "bg3.jpg",
            "bg4.jpg",
            "bg5.jpg"
        };

        const string outputPdf = "output.pdf";

        // Verify all image files exist before proceeding
        foreach (string imgPath in bgImages)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Missing image: {imgPath}");
                return;
            }
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add five pages, one for each background image
            for (int i = 0; i < bgImages.Length; i++)
            {
                // Add a new blank page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages.Add();

                // Create an ImageStamp that will act as a background image
                ImageStamp bgStamp = new ImageStamp(bgImages[i])
                {
                    // Place the stamp behind existing page content
                    Background = true,

                    // Stretch the image to fill the entire page
                    // Page dimensions are in points (1 point = 1/72 inch)
                    // Use the page's width and height for exact fit
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };

                // Apply the background stamp to the current page
                page.AddStamp(bgStamp);
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with 5 pages: {outputPdf}");
    }
}