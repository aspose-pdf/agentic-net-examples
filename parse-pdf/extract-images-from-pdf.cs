using System;
using System.IO;
using System.Drawing.Imaging;
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
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over the XImage collection
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"image_page{pageIndex}_{imageCounter}.png");

                    // Save the embedded image to a file stream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image {imageCounter} from page {pageIndex} to '{outputPath}'");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
