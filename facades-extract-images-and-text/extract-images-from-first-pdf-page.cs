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

        try
        {
            List<byte[]> imageBytes = ExtractImagesFromFirstPage(inputPdf);

            Console.WriteLine($"Extracted {imageBytes.Count} image(s) from the first page.");
            // Example: process the byte arrays (e.g., analyze, save, etc.)
            for (int i = 0; i < imageBytes.Count; i++)
            {
                Console.WriteLine($"Image {i + 1}: {imageBytes[i].Length} bytes");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Extracts all images from the first page of a PDF and returns them as byte arrays.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <returns>List of byte arrays, each containing an extracted image.</returns>
    static List<byte[]> ExtractImagesFromFirstPage(string pdfPath)
    {
        var images = new List<byte[]>();

        // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // Limit extraction to the first page (pages are 1‑based).
            extractor.StartPage = 1;
            extractor.EndPage = 1;

            // Perform the image extraction.
            extractor.ExtractImage();

            // Retrieve each image into a memory stream and store its bytes.
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // GetNextImage returns a bool indicating success; ignore it here.
                    extractor.GetNextImage(ms);
                    images.Add(ms.ToArray());
                }
            }
        }

        return images;
    }
}