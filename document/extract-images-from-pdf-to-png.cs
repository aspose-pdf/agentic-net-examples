using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat.Png
using Aspose.Pdf;               // Document, Page, XImage

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPdf))
        {
            int imageIndex = 1;

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all images defined in the page resources
                foreach (XImage xImg in page.Resources.Images)
                {
                    string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                    // Save the image as PNG, preserving its original resolution
                    // XImage supports saving to a stream or file with a specified ImageFormat
                    using (FileStream fs = new FileStream(outPath, FileMode.Create))
                    {
                        xImg.Save(fs, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}