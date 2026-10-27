using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string bannerImgPath = "banner.jpg";    // banner image file
        const string outputPdfPath = "output.pdf";    // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bannerImgPath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImgPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            int lastPageNumber = pdfDoc.Pages.Count;
            Page lastPage = pdfDoc.Pages[lastPageNumber];

            // ------------------------------------------------------------
            // Remove all existing images from the last page
            // ------------------------------------------------------------
            // Collect images first to avoid modifying the collection while iterating
            List<XImage> imagesToRemove = new List<XImage>();
            foreach (XImage img in lastPage.Resources.Images)
                imagesToRemove.Add(img);

            foreach (XImage img in imagesToRemove)
                lastPage.Resources.Images.Remove(img);

            // ------------------------------------------------------------
            // Add a banner that spans the full page width
            // ------------------------------------------------------------
            ImageStamp bannerStamp = new ImageStamp(bannerImgPath)
            {
                // Set the banner width to match the page width
                Width = lastPage.Rect.Width,

                // Keep the original aspect ratio; height will be scaled automatically
                // If you prefer a fixed height, set Height and set FixHeight = true
                // Height = 100; // example fixed height
                // FixHeight = true;

                // Align the banner at the top of the page (you can change as needed)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top
            };

            // Apply the stamp to the page
            lastPage.AddStamp(bannerStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Banner applied and saved to '{outputPdfPath}'.");
    }
}