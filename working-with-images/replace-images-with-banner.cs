using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithBanner
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string bannerImg = "banner.jpg"; // banner image (JPEG)
        const string outputPdf = "output.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bannerImg))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImg}");
            return;
        }

        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the last page (page indexing is 1‑based)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Remove all existing images on the last page.
            // The ImageAbsorber class is not available in the current Aspose.Pdf version,
            // so we clear the image resources collection directly.
            if (lastPage.Resources != null && lastPage.Resources.Images != null)
            {
                lastPage.Resources.Images.Clear();
            }

            // Add the banner image spanning the full page width (and height)
            using (FileStream bannerStream = File.OpenRead(bannerImg))
            {
                // Rectangle covering the whole page (lower‑left origin)
                Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                    0,                                 // llx
                    0,                                 // lly
                    lastPage.Rect.Width,               // urx (page width)
                    lastPage.Rect.Height);            // ury (page height)

                // AddImage places the image inside the rectangle preserving aspect ratio.
                // Using the full page rectangle makes the banner fill the page width.
                lastPage.AddImage(bannerStream, pageRect);
            }

            // Save the modified document (inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Banner applied and saved to '{outputPdf}'.");
    }
}
