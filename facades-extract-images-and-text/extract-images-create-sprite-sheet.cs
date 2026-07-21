using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;               // System.Drawing.Imaging.ImageFormat for extraction
using Aspose.Pdf.Facades;                  // PdfExtractor, PdfConverter, ImageMergeMode
using Aspose.Pdf;                          // ExtractImageMode (if needed)
using Aspose.Pdf.Drawing;                  // Aspose.Pdf.Drawing.ImageFormat for merging

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

        // Collect extracted images in memory streams
        List<Stream> imageStreams = new List<Stream>();

        // Extract images from the PDF using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: extract only actually used images
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                // Store each image as PNG in a memory stream
                MemoryStream ms = new MemoryStream();
                // GetNextImage expects System.Drawing.Imaging.ImageFormat
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

        // Merge all images into a single sprite sheet (horizontal layout)
        // PdfConverter.MergeImages returns a Stream, not a MemoryStream.
        using (Stream merged = PdfConverter.MergeImages(
            imageStreams,
            // MergeImages expects Aspose.Pdf.Drawing.ImageFormat
            Aspose.Pdf.Drawing.ImageFormat.Png,
            ImageMergeMode.Horizontal,
            null,
            null))
        {
            // Save the merged sprite sheet to a PNG file
            using (FileStream outFile = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
            {
                merged.CopyTo(outFile);
            }
        }

        // Dispose individual image streams
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"Sprite sheet created: {outputPng}");
    }
}
