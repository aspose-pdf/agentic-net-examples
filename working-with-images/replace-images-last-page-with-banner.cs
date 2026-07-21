using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string bannerPath = "banner.jpg";   // JPEG banner image
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(bannerPath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the last page (1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Hide all existing images on the last page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            lastPage.Accept(imgAbsorber);
            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                placement.Hide(); // removes the image from the page
            }

            // Determine banner rectangle – full page width, fixed height (e.g., 100 points)
            const double bannerHeight = 100.0;
            Aspose.Pdf.Rectangle bannerRect = new Aspose.Pdf.Rectangle(
                lastPage.Rect.LLX,                     // left
                lastPage.Rect.LLY,                     // bottom
                lastPage.Rect.URX,                     // right
                lastPage.Rect.LLY + bannerHeight);     // top

            // Add the banner image to the page
            using (FileStream bannerStream = File.OpenRead(bannerPath))
            {
                // AddImage places the image proportionally inside the rectangle
                lastPage.AddImage(bannerStream, bannerRect);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with banner: {outputPath}");
    }
}