using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bannerImgPath = "banner.jpg"; // JPEG banner image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(bannerImgPath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImgPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (page indexing is 1‑based)
            int lastPageIndex = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageIndex];

            // Hide all existing images on the last page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            lastPage.Accept(imgAbsorber);
            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                placement.Hide(); // removes the image from the page
            }

            // Add the banner image spanning the full page width
            using (FileStream bannerStream = File.OpenRead(bannerImgPath))
            {
                // Full page rectangle
                Aspose.Pdf.Rectangle pageRect = lastPage.Rect;

                // Define banner rectangle: full width, fixed height (e.g., 100 points) at the bottom of the page
                double bannerHeight = 100; // adjust as needed
                Aspose.Pdf.Rectangle bannerRect = new Aspose.Pdf.Rectangle(
                    pageRect.LLX,                     // left
                    pageRect.LLY,                     // bottom
                    pageRect.URX,                     // right
                    pageRect.LLY + bannerHeight);    // top

                // Add the banner image to the page
                lastPage.AddImage(bannerStream, bannerRect);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with banner on last page: {outputPdfPath}");
    }
}