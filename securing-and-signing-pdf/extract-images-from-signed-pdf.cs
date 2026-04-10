using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging;

class ExtractImagesFromSignedPdf
{
    static void Main()
    {
        const string inputPdfPath = "signed_input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (no special load options required)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all images in the page's resources
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"Page{pageIndex}_Image{imageCounter}.png");

                    // Aspose.Pdf XImage.Save only accepts a Stream. Save to a MemoryStream first,
                    // then re‑encode the image as PNG using System.Drawing (available on Windows).
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);               // Save original image bytes to memory
                        ms.Position = 0;            // Reset stream position for reading
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(ms))
                        {
                            sysImg.Save(outputPath, ImageFormat.Png);
                        }
                    }

                    Console.WriteLine($"Saved image {imageCounter} from page {pageIndex} to '{outputPath}'");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
