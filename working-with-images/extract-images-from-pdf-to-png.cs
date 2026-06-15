using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging; // for ImageFormat.Png

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over the image resources of the current page
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the image using the Stream overload (XImage.Save expects a Stream, not a file path)
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
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
