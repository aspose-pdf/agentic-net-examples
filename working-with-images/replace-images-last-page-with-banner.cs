using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImagePlacementAbsorber

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string bannerImgPath = "banner.jpg";    // banner image (JPEG)

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Hide all existing images on the last page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            lastPage.Accept(imgAbsorber);
            foreach (ImagePlacement imgPlacement in imgAbsorber.ImagePlacements)
            {
                imgPlacement.Hide(); // removes the image from the page content
            }

            // Add the banner image so it spans the full width of the page
            using (FileStream bannerStream = File.OpenRead(bannerImgPath))
            {
                // Define a rectangle that covers the entire page area
                // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                    0,                                   // lower‑left X
                    0,                                   // lower‑left Y
                    lastPage.MediaBox.Width,             // upper‑right X (page width)
                    lastPage.MediaBox.Height);           // upper‑right Y (page height)

                // Add the banner image; the overload preserves aspect ratio
                lastPage.AddImage(bannerStream, pageRect);
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner applied and saved to '{outputPdfPath}'.");
    }
}