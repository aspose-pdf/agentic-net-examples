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

        // Container for the extracted image byte arrays
        List<byte[]> extractedImages = new List<byte[]>();

        // PdfExtractor is the Facade class for image extraction
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(pdfPath);

            // Limit extraction to the first page only (1‑based indexing)
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // Perform the image extraction operation
            extractor.ExtractImage();

            // Retrieve each image as a byte array
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Store the next image into the memory stream (default JPEG format)
                    extractor.GetNextImage(ms);

                    // Reset stream position before reading
                    ms.Position = 0;

                    // Add the image bytes to the list
                    extractedImages.Add(ms.ToArray());
                }
            }
        }

        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from the first page.");
        // The 'extractedImages' list now holds the raw image data for further analysis.
    }
}