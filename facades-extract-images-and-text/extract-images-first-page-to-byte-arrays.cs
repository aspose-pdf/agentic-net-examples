using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // List to hold extracted image byte arrays
        List<byte[]> extractedImages = new List<byte[]>();

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Restrict extraction to the first page
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // Extract images from the specified page range
            extractor.ExtractImage();

            // Iterate over all images found on the first page
            while (extractor.HasNextImage())
            {
                // Store each image into a memory stream (default format is JPEG)
                using (MemoryStream ms = new MemoryStream())
                {
                    // Retrieve the next image into the stream
                    extractor.GetNextImage(ms);

                    // Add the byte array to the collection for further analysis
                    extractedImages.Add(ms.ToArray());
                }
            }
        }

        // Example usage of the extracted byte arrays
        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from the first page.");
        // Further processing can be done with the byte arrays in 'extractedImages'
    }
}