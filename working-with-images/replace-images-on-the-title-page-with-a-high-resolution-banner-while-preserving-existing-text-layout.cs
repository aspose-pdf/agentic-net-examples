using System;
using System.IO;
using Aspose.Pdf;

class ReplaceTitlePageImages
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF with title page
        const string outputPdfPath = "output.pdf";         // result PDF
        const string bannerPath    = "high_res_banner.jpg"; // high‑resolution banner image

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bannerPath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerPath}");
            return;
        }

        // Load the PDF document (using block guarantees proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Title page is the first page (Aspose.Pdf uses 1‑based indexing)
            Page titlePage = doc.Pages[1];

            // Access the image collection of the page
            XImageCollection images = titlePage.Resources.Images;

            // If there are no images on the title page, nothing to replace
            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the title page.");
            }
            else
            {
                // Load banner bytes once – reuse for each replacement
                byte[] bannerBytes = File.ReadAllBytes(bannerPath);

                // Replace each existing image with the high‑resolution banner
                for (int i = 1; i <= images.Count; i++)
                {
                    // Create a fresh stream for each Replace call (the method consumes the stream)
                    using (MemoryStream ms = new MemoryStream(bannerBytes))
                    {
                        images.Replace(i, ms);
                    }
                }

                Console.WriteLine($"Replaced {images.Count} image(s) on the title page.");
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Output saved to '{outputPdfPath}'.");
    }
}