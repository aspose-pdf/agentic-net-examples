using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string bannerPath = "banner.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bannerPath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerPath}");
            return;
        }

        // Load banner image bytes once to reuse for each replacement
        byte[] bannerBytes = File.ReadAllBytes(bannerPath);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Index page is assumed to be the first page (1‑based indexing)
            Page indexPage = doc.Pages[1];
            var images = indexPage.Resources.Images;

            // Replace every image on the index page with the banner image
            for (int i = 1; i <= images.Count; i++)
            {
                using (MemoryStream ms = new MemoryStream(bannerBytes))
                {
                    images.Replace(i, ms);
                }
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated. Output saved to '{outputPdf}'.");
    }
}