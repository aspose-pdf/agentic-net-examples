using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string bannerImgPath  = "high_res_banner.jpg";

        // Validate file existence
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

        // Load the source PDF (title page is assumed to be the first page)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Get the first page (1‑based indexing)
            Page titlePage = pdfDoc.Pages[1];

            // Access the image collection of the page
            XImageCollection images = titlePage.Resources.Images;

            // If there are no images, nothing to replace
            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the title page.");
            }
            else
            {
                // Load banner image once into memory
                byte[] bannerBytes = File.ReadAllBytes(bannerImgPath);

                // Replace each existing image with the banner while preserving layout
                for (int i = 1; i <= images.Count; i++)
                {
                    // Create a fresh stream for each replacement (Replace expects a readable stream)
                    using (MemoryStream bannerStream = new MemoryStream(bannerBytes))
                    {
                        images.Replace(i, bannerStream);
                    }

                    // Optional: set alternative text for accessibility
                    // Note: TrySetAlternativeText returns false if the image appears multiple times;
                    // here we ignore the return value for brevity.
                    XImage replacedImg = images[i];
                    replacedImg.TrySetAlternativeText("High‑resolution banner", titlePage);
                }

                Console.WriteLine($"Replaced {images.Count} image(s) on the title page with the banner.");
            }

            // Save the modified PDF (PDF format – no SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Output PDF saved to '{outputPdfPath}'.");
    }
}