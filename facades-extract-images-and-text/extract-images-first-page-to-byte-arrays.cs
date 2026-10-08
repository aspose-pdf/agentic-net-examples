using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold extracted image byte arrays
        List<byte[]> images = new List<byte[]>();

        // PdfExtractor (Aspose.Pdf.Facades) extracts images from PDFs
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF file to the extractor
        extractor.BindPdf(inputPath);

        // Restrict extraction to the first page only
        extractor.StartPage = 1;
        extractor.EndPage   = 1;

        // Perform the image extraction
        extractor.ExtractImage();

        // Retrieve each image as a byte array using a MemoryStream
        while (extractor.HasNextImage())
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // GetNextImage writes the image data into the provided stream
                extractor.GetNextImage(ms);
                byte[] imageBytes = ms.ToArray();
                images.Add(imageBytes);
            }
        }

        Console.WriteLine($"Extracted {images.Count} image(s) from page 1.");

        // Optional: write each image to a file for verification
        for (int i = 0; i < images.Count; i++)
        {
            string outPath = $"page1_image_{i + 1}.bin";
            File.WriteAllBytes(outPath, images[i]);
            Console.WriteLine($"Saved image {i + 1} to {outPath}");
        }
    }
}
