using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // original document
        const string bannerImg  = "banner.jpg";     // new branding image (JPEG)
        const string outputPdf  = "output.pdf";     // result document

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get the first (index) page
            Page indexPage = doc.Pages[1];

            // Access the image collection of the page
            var images = indexPage.Resources.Images;

            // Open the banner image stream once
            using (FileStream bannerStream = File.OpenRead(bannerImg))
            {
                // Replace each existing image on the page with the banner
                // XImageCollection uses 1‑based indexing
                for (int i = 1; i <= images.Count; i++)
                {
                    // Reset stream position for each replacement
                    bannerStream.Position = 0;
                    images.Replace(i, bannerStream);
                }
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated. Saved to '{outputPdf}'.");
    }
}