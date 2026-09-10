using System;
using System.IO;
using Aspose.Pdf;

class ReplaceTitlePageBanner
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string bannerImagePath = "banner_high_res.jpg"; // high‑resolution banner

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Assume the title page is the first page (1‑based indexing)
            Page titlePage = doc.Pages[1];

            // Access the image collection of the page
            var images = titlePage.Resources.Images;

            // If there are images on the title page, replace each with the banner
            for (int i = 1; i <= images.Count; i++)
            {
                // Open a fresh stream for each replacement (Replace consumes the stream)
                using (FileStream bannerStream = File.OpenRead(bannerImagePath))
                {
                    images.Replace(i, bannerStream);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner replacement completed. Saved to '{outputPdfPath}'.");
    }
}