using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Container for the extracted image byte arrays
        List<byte[]> extractedImages = new List<byte[]>();

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (a Facade) to pull images from the first page only
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Restrict extraction to page 1
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // Perform the image extraction operation
            extractor.ExtractImage();

            // Retrieve each image as a byte array
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Store the current image into the memory stream (default format)
                    extractor.GetNextImage(ms);

                    // Add the resulting byte array to the list
                    extractedImages.Add(ms.ToArray());
                }
            }
        }

        // Example output: number of images and their sizes
        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from page 1.");
        for (int i = 0; i < extractedImages.Count; i++)
        {
            Console.WriteLine($"Image {i + 1}: {extractedImages[i].Length} bytes");
        }

        // The 'extractedImages' list now holds the raw image data for further analysis
    }
}
