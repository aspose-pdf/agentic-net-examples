using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with title page
        const string bannerImg  = "banner_high_res.jpg"; // high‑resolution banner image
        const string outputPdf  = "output.pdf";         // result PDF

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

        // Load the PDF document (lifecycle: using ensures disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Assume the title page is the first page (1‑based indexing)
            Page titlePage = doc.Pages[1];

            // Replace the first image in the page's image collection with the high‑resolution banner
            // XImageCollection.Replace uses 1‑based index for the image to replace
            using (FileStream bannerStream = File.OpenRead(bannerImg))
            {
                titlePage.Resources.Images.Replace(1, bannerStream);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Banner replaced and saved to '{outputPdf}'.");
    }
}