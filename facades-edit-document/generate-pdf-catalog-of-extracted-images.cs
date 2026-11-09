using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";
        const string catalogPdfPath = "image_catalog.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Create an empty PDF that will hold the image catalog
            using (Document catalogDoc = new Document())
            {
                // Iterate over all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= sourceDoc.Pages.Count; pageIndex++)
                {
                    Page srcPage = sourceDoc.Pages[pageIndex];

                    // Iterate over each image resource on the page
                    foreach (XImage xImg in srcPage.Resources.Images)
                    {
                        // Export the XImage to a memory stream (PNG format works for all image types)
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // XImage.Save writes the raw image bytes; the format is preserved
                            xImg.Save(imgStream);
                            imgStream.Position = 0; // reset for reading

                            // Create a new page in the catalog PDF for this thumbnail
                            Page catalogPage = catalogDoc.Pages.Add();

                            // Create an ImageStamp from the image stream
                            ImageStamp imgStamp = new ImageStamp(imgStream);

                            // Define thumbnail size (adjust as needed)
                            const double thumbWidth = 200;
                            const double thumbHeight = 200;

                            imgStamp.Width = thumbWidth;
                            imgStamp.Height = thumbHeight;

                            // Center the thumbnail on the page
                            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                            // Add the stamp to the newly created page
                            catalogPage.AddStamp(imgStamp);
                        }
                    }
                }

                // Save the catalog PDF
                catalogDoc.Save(catalogPdfPath);
                Console.WriteLine($"Image catalog saved to '{catalogPdfPath}'.");
            }
        }
    }
}