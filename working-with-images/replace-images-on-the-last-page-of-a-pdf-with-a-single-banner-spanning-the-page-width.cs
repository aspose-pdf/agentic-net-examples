using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImagePlacementAbsorber

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string bannerImgPath = "banner.jpg";   // JPEG banner image
        const string outputPdfPath = "output.pdf";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
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

            // Determine page width from MediaBox
            Aspose.Pdf.Rectangle mediaBox = lastPage.MediaBox;
            double pageWidth  = mediaBox.URX - mediaBox.LLX;
            double pageHeight = mediaBox.URY - mediaBox.LLY;

            // Define banner height (e.g., 100 points)
            double bannerHeight = 100;

            // Position banner at the top of the page
            Aspose.Pdf.Rectangle bannerRect = new Aspose.Pdf.Rectangle(
                mediaBox.LLX,                     // left
                mediaBox.URY - bannerHeight,      // bottom (top minus height)
                mediaBox.URX,                     // right
                mediaBox.URY);                    // top

            // Add the banner image spanning the full page width
            using (FileStream bannerStream = File.OpenRead(bannerImgPath))
            {
                lastPage.AddImage(bannerStream, bannerRect);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with banner on last page: {outputPdfPath}");
    }
}