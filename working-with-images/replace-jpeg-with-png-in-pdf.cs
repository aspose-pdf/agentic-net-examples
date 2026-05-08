using System;
using System.IO;
using Aspose.Pdf;

class ReplaceJpegWithPng
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string pngReplacementPath = "replacement.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pngReplacementPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {pngReplacementPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // XImageCollection does not provide an indexer for direct access,
                // but it supports 1‑based indexing via the collection itself.
                // Replace each image resource with the PNG stream.
                int imageCount = page.Resources.Images.Count;
                for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                {
                    // Load the PNG data into a memory stream
                    using (FileStream pngStream = File.OpenRead(pngReplacementPath))
                    {
                        // Replace the image at the current index.
                        // The Replace method expects a stream containing the new image data.
                        page.Resources.Images.Replace(imgIndex, pngStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with PNG replacements: {outputPdfPath}");
    }
}