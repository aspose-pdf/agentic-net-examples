using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_monochrome.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                XImageCollection images = page.Resources.Images;

                // Iterate over images (also 1‑based)
                for (int i = 1; i <= images.Count; i++)
                {
                    XImage img = images[i];

                    // Obtain a grayscale version of the image (System.Drawing.Image)
                    System.Drawing.Image gray = img.Grayscaled;

                    // Encode the grayscale image as JPEG into a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        gray.Save(ms, ImageFormat.Jpeg);
                        ms.Position = 0; // reset stream position for replacement

                        // Replace the original image with the grayscale JPEG
                        images.Replace(i, ms);
                    }

                    // Dispose the temporary grayscale bitmap
                    gray.Dispose();
                }
            }

            // Save the modified PDF (no special SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}
