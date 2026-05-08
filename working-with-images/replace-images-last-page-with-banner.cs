using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string bannerImg = "banner.jpg";     // banner image (JPEG)
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bannerImg))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImg}");
            return;
        }

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Hide all existing images on the last page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            lastPage.Accept(imgAbsorber);
            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                placement.Hide();   // removes the image from the page
            }

            // Add the banner image spanning the full page width
            using (FileStream bannerStream = File.OpenRead(bannerImg))
            {
                // Use the page rectangle (media box) as the target area
                Aspose.Pdf.Rectangle pageRect = lastPage.Rect;
                lastPage.AddImage(bannerStream,
                                  new Aspose.Pdf.Rectangle(pageRect.LLX, pageRect.LLY,
                                                          pageRect.URX, pageRect.URY));
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Banner applied and saved to '{outputPdf}'.");
    }
}