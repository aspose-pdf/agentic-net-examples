using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfExtractor lives here
using Aspose.Pdf.Drawing;          // ImageStamp lives here

class ImageCatalogGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "catalog.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the source PDF using PdfExtractor.
        // -----------------------------------------------------------------
        List<byte[]> extractedImages = new List<byte[]>();

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream (newer API requires a stream).
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    extractedImages.Add(imgStream.ToArray());
                }
            }
        }

        // -----------------------------------------------------------------
        // 2. Create a new PDF document that will serve as the image catalog.
        // -----------------------------------------------------------------
        using (Document catalog = new Document())
        {
            // For each extracted image, add a new page with a thumbnail.
            foreach (byte[] imgData in extractedImages)
            {
                // Add a blank page to the catalog.
                Page page = catalog.Pages.Add();

                // Create a memory stream for the image data.
                using (MemoryStream imgStream = new MemoryStream(imgData))
                {
                    // Create an ImageStamp from the stream (Aspose.Pdf.Drawing namespace).
                    ImageStamp stamp = new ImageStamp(imgStream)
                    {
                        // Set thumbnail width; height scales proportionally.
                        Width = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        // Ensure the stamp is placed over page content.
                        Background = false
                    };

                    // Place the stamp on the page.
                    page.AddStamp(stamp);
                }
            }

            // Save the catalog PDF.
            catalog.Save(outputPdfPath);
        }

        Console.WriteLine($"Image catalog created: {outputPdfPath}");
    }
}
