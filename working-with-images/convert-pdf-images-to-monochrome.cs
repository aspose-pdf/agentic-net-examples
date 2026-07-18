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
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Access the image collection of the current page
                var images = page.Resources.Images;
                int imageCount = images.Count;

                // Replace each image with its grayscale version
                for (int i = 1; i <= imageCount; i++)
                {
                    XImage original = images[i];

                    // Obtain a grayscale System.Drawing.Image (fully qualified to avoid ambiguity)
                    System.Drawing.Image grayImage = original.Grayscaled;

                    // Write the grayscale image to a memory stream (JPEG format)
                    using (MemoryStream ms = new MemoryStream())
                    {
                        grayImage.Save(ms, ImageFormat.Jpeg);
                        ms.Position = 0; // Reset stream position for reading

                        // Replace the original image in the collection with the grayscale stream
                        images.Replace(i, ms);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}
