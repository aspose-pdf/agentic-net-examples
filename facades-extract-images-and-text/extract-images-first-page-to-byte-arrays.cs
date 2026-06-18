using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Collection that will hold the extracted images as byte arrays
        List<byte[]> extractedImages = new List<byte[]>();

        // PdfExtractor is a Facade that handles image extraction
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file into the extractor
            extractor.BindPdf(pdfPath);

            // Limit extraction to the first page (pages are 1‑based)
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // NOTE: The ExtractImageMode property does not exist in the current
            // Aspose.Pdf.Facades version. The default behaviour of ExtractImage()
            // extracts all images on the selected pages, which is sufficient for
            // this scenario.
            extractor.ExtractImage();

            // Retrieve each image and store it in a memory stream
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Get the next image; default format is JPEG
                    extractor.GetNextImage(ms);

                    // Save the image bytes for later analysis
                    extractedImages.Add(ms.ToArray());
                }
            }
        }

        // Example output: number of images and their byte sizes
        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from page 1.");
        for (int i = 0; i < extractedImages.Count; i++)
        {
            Console.WriteLine($"Image {i + 1}: {extractedImages[i].Length} bytes");
        }

        // The 'extractedImages' list now contains the raw image data for further processing
    }
}
