using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all raster images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each image
                    string imageFileName = $"image_{imageCounter:D4}.png";
                    string imagePath = Path.Combine(outputFolder, imageFileName);

                    // Save the image as PNG using a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image {imageCounter} to '{imagePath}'");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
