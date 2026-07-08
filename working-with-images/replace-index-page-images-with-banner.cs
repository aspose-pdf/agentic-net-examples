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

            // Replace every existing image on the index page with the banner image
            // The Replace method expects a 1‑based index and a Stream containing JPEG data
            for (int i = 1; i <= images.Count; i++)
            {
                using (FileStream bannerStream = File.OpenRead(bannerImg))
                {
                    images.Replace(i, bannerStream);
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated. Output saved to '{outputPdf}'.");
    }
}