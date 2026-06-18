using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // needed for PdfExtractor.GetNextImage
using Aspose.Pdf.Drawing;   // needed for PdfConverter.MergeImages

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "sprite.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract all images from the PDF into memory streams (PNG format)
        // -----------------------------------------------------------------
        List<Stream> imageStreams = new List<Stream>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                // Each image is written to a MemoryStream as PNG
                MemoryStream ms = new MemoryStream();
                // Fully qualified to avoid ambiguity with Aspose.Pdf.Drawing.ImageFormat
                extractor.GetNextImage(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;               // reset for later reading
                imageStreams.Add(ms);
            }
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Merge the extracted images into a single sprite sheet (horizontal)
        // ---------------------------------------------------------------
        // ImageMergeMode.Horizontal arranges images side‑by‑side.
        using (Stream mergedStream = PdfConverter.MergeImages(
            imageStreams,
            Aspose.Pdf.Drawing.ImageFormat.Png, // Aspose.Pdf.Drawing.ImageFormat
            ImageMergeMode.Horizontal,
            null,   // optional spacing between images (null = default)
            null)) // optional max width/height (null = auto)
        {
            // -----------------------------------------------------------
            // 3. Save the merged sprite sheet to a PNG file
            // -----------------------------------------------------------
            using (FileStream outFile = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
            {
                mergedStream.CopyTo(outFile);
            }
        }

        // Clean up individual image streams
        foreach (var s in imageStreams)
        {
            s.Dispose();
        }

        Console.WriteLine($"Sprite sheet created: {outputPng}");
    }
}
