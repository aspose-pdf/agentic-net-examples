using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_monochrome.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                // XImageCollection is 1‑based as well
                for (int i = 1; i <= page.Resources.Images.Count; i++)
                {
                    XImage xImg = page.Resources.Images[i];

                    // Replace condition – currently replaces every image.
                    if (ContainsTargetPalette(xImg))
                    {
                        // Obtain a grayscale version of the image (System.Drawing.Image)
                        System.Drawing.Image gray = xImg.Grayscaled;

                        // Encode the grayscale image to JPEG in a memory stream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            gray.Save(ms, ImageFormat.Jpeg);
                            ms.Position = 0; // reset stream position for replacement

                            // Replace the original image with the grayscale JPEG
                            page.Resources.Images.Replace(i, ms);
                        }

                        // Dispose the temporary grayscale image
                        gray.Dispose();
                    }
                }
            }

            // Save the modified PDF (save rule: direct Save for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }

    // Dummy implementation – replace with actual palette detection logic
    static bool ContainsTargetPalette(XImage image)
    {
        // Example: replace all images (real implementation would inspect the palette)
        return true;
    }
}
