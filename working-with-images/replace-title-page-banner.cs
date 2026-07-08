using System;
using System.IO;
using Aspose.Pdf;

class ReplaceTitlePageBanner
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // original PDF with title page
        const string outputPdfPath  = "output.pdf";         // PDF after banner replacement
        const string bannerImagePath = "high_res_banner.jpg"; // high‑resolution banner image

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bannerImagePath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImagePath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Title page is the first page (Aspose.Pdf uses 1‑based indexing)
            Page titlePage = pdfDoc.Pages[1];

            // Access the image collection of the page
            XImageCollection images = titlePage.Resources.Images;

            // If there are no images on the title page, nothing to replace
            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the title page. No replacement performed.");
            }
            else
            {
                // Replace the first image with the high‑resolution banner.
                // XImageCollection.Replace expects a 1‑based index and a Stream containing JPEG data.
                using (FileStream bannerStream = File.OpenRead(bannerImagePath))
                {
                    // Replace image at index 1 (first image). Adjust index if a specific image is required.
                    images.Replace(1, bannerStream);
                }

                // Optionally, if you want to replace all images on the title page with the same banner:
                // for (int i = 1; i <= images.Count; i++)
                // {
                //     using (FileStream bannerStream = File.OpenRead(bannerImagePath))
                //     {
                //         images.Replace(i, bannerStream);
                //     }
                // }
            }

            // Save the modified document (PDF format). No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner replacement completed. Output saved to '{outputPdfPath}'.");
    }
}