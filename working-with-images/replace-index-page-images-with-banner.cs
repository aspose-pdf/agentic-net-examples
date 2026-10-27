using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source document
        const string outputPdf  = "output.pdf";     // destination document
        const string bannerImg  = "banner.jpg";     // new branding image

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bannerImg))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Assume the index page is the first page (1‑based indexing)
            Page indexPage = doc.Pages[1];

            // Remove all existing images from the page
            // XImageCollection supports Clear()
            indexPage.Resources.Images.Clear();

            // Create an ImageStamp for the new banner image
            ImageStamp bannerStamp = new ImageStamp(bannerImg)
            {
                // Stretch the banner to fill the page width while preserving aspect ratio
                Width  = indexPage.PageInfo.Width,
                Height = indexPage.PageInfo.Height,
                // Align at the top of the page; adjust as needed
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Ensure the image is placed behind any existing content (optional)
                Background = true
            };

            // Add the banner to the index page
            indexPage.AddStamp(bannerStamp);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated. Output saved to '{outputPdf}'.");
    }
}