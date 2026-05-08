using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bannerPath = "banner.jpg";

        if (!File.Exists(inputPdf) || !File.Exists(bannerPath))
        {
            Console.Error.WriteLine("Input PDF or banner image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Title page is assumed to be the first page (1‑based indexing)
            Page titlePage = doc.Pages[1];

            // Access the image collection of the page
            XImageCollection images = titlePage.Resources.Images;

            // Replace every image on the title page with the high‑resolution banner
            for (int i = 1; i <= images.Count; i++)
            {
                // Open the banner image as a stream (JPEG format expected)
                using (FileStream bannerStream = File.OpenRead(bannerPath))
                {
                    // XImageCollection.Replace replaces the image at the given index
                    images.Replace(i, bannerStream);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Banner replaced on title page. Saved to '{outputPdf}'.");
    }
}