using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing internally,
            // but foreach abstracts that away)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over the image resources of the current page.
                // XImageCollection is not a dictionary; direct foreach is required.
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image.
                    string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the XImage as PNG using a FileStream because the overload
                    // XImage.Save expects a Stream, not a file path string.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image {imageIndex} → {outputPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All raster images have been extracted.");
    }
}
