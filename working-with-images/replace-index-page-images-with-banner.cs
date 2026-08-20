using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // original document
        const string outputPdfPath = "output.pdf";         // updated document
        const string bannerImagePath = "banner.jpg";       // new branding image (JPEG)

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
            // Assume the index page is the first page (1‑based indexing)
            Page indexPage = doc.Pages[1];

            // Access the image collection of the index page
            XImageCollection images = indexPage.Resources.Images;

            // Read the banner image once into a byte array (JPEG format)
            byte[] bannerBytes = File.ReadAllBytes(bannerImagePath);

            // Replace every existing image on the index page with the banner image
            for (int i = 1; i <= images.Count; i++)
            {
                // Create a fresh stream for each replacement (Replace consumes the stream)
                using (MemoryStream ms = new MemoryStream(bannerBytes))
                {
                    images.Replace(i, ms);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Branding updated. Saved to '{outputPdfPath}'.");
    }
}