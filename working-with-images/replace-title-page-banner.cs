using System;
using System.IO;
using Aspose.Pdf;

class ReplaceTitlePageImages
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string bannerImagePath = "banner_high_res.jpg";
        const string outputPdfPath  = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page (the title page).
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document does not contain any pages.");
                return;
            }

            // Get the title page (first page – 1‑based indexing).
            Page titlePage = doc.Pages[1];

            // OPTIONAL: Remove existing images from the page.
            // The XImageCollection does not expose a direct Remove method,
            // so we clear the collection by creating a new one.
            // This step is safe because we will overlay the new banner.
            titlePage.Resources.Images.Clear();

            // Create an ImageStamp for the high‑resolution banner.
            ImageStamp bannerStamp = new ImageStamp(bannerImagePath)
            {
                // Place the banner on top of existing content (Background = false).
                Background = false,
                // Align the banner horizontally centered and vertically at the top.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Optionally set the width/height to fit the page width.
                // If you want the banner to span the full page width:
                // Width = titlePage.PageInfo.Width,
                // Height = titlePage.PageInfo.Height * 0.2 // 20% of page height, adjust as needed.
            };

            // Add the banner stamp to the title page.
            titlePage.AddStamp(bannerStamp);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}