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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // XImageCollection uses 1‑based indexing
                int imageIndex = 1;

                // Iterate over each image resource on the page
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Obtain a grayscale version of the image (System.Drawing.Image)
                    using (System.Drawing.Image grayImg = xImg.Grayscaled)
                    {
                        // Encode the grayscale image to a JPEG stream
                        using (MemoryStream grayStream = new MemoryStream())
                        {
                            grayImg.Save(grayStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            grayStream.Position = 0; // reset for reading

                            // Replace the original image with the grayscale JPEG
                            page.Resources.Images.Replace(imageIndex, grayStream);
                        }
                    }

                    imageIndex++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}
