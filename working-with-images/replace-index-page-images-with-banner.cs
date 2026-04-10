using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string bannerImg = "banner.jpg";         // new branding image (JPEG)

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Assume the index page is the first page (1‑based indexing)
            Page indexPage = doc.Pages[1];

            // Access the image collection of the page
            XImageCollection images = indexPage.Resources.Images;

            // If there are images on the page, replace each with the banner image
            if (images.Count > 0)
            {
                // Load banner image into a memory stream (JPEG format required by Replace)
                using (FileStream bannerStream = File.OpenRead(bannerImg))
                {
                    // Replace every existing image with the banner
                    for (int i = 1; i <= images.Count; i++)
                    {
                        // Reset stream position for each replacement
                        bannerStream.Position = 0;
                        images.Replace(i, bannerStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated. Saved to '{outputPdf}'.");
    }
}