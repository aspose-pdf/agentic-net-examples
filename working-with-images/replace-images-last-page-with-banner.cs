using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bannerImgPath = "banner.jpg";

        // Verify required files exist
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // -----------------------------------------------------------------
            // Step 1: Hide all existing images on the last page
            // -----------------------------------------------------------------
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            lastPage.Accept(absorber);
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                placement.Hide(); // removes the image from the page content
            }

            // -----------------------------------------------------------------
            // Step 2: Add the banner image spanning the full page width
            // -----------------------------------------------------------------
            // Load the banner once; the stream will be reused for AddImage
            using (FileStream bannerStream = File.OpenRead(bannerImgPath))
            {
                // Determine the page rectangle (full media box)
                Aspose.Pdf.Rectangle pageRect = lastPage.Rect;

                // Define a rectangle that spans the full width.
                // Height is set to 20 % of the page height – adjust as needed.
                double bannerHeight = (pageRect.URY - pageRect.LLY) * 0.20;
                Aspose.Pdf.Rectangle bannerRect = new Aspose.Pdf.Rectangle(
                    pageRect.LLX,                     // left
                    pageRect.LLY,                     // bottom
                    pageRect.URX,                     // right
                    pageRect.LLY + bannerHeight);     // top

                // Add the banner image; Aspose.Pdf will preserve aspect ratio.
                lastPage.AddImage(bannerStream, bannerRect);
            }

            // -----------------------------------------------------------------
            // Step 3: Save the modified document (lifecycle rule: save inside using)
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner applied to the last page and saved as '{outputPdfPath}'.");
    }
}