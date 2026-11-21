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

        // List to hold the extracted image data as byte arrays
        List<byte[]> extractedImages = new List<byte[]>();

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(pdfPath);

            // Restrict extraction to the first page only (1‑based index)
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // Perform image extraction for the selected page range
            // (ExtractImageMode property does not exist in the referenced version)
            extractor.ExtractImage();

            // Iterate through the extracted images using the supported API
            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // Get the next image into the memory stream
                    extractor.GetNextImage(imgStream);
                    extractedImages.Add(imgStream.ToArray());
                }
            }
        }

        // Report the results (or pass the byte arrays to further analysis)
        Console.WriteLine($"Extracted {extractedImages.Count} image(s) from page 1.");
        for (int i = 0; i < extractedImages.Count; i++)
        {
            Console.WriteLine($"Image {i + 1}: {extractedImages[i].Length} bytes");
        }
    }
}
