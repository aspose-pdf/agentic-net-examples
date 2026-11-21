using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    // Convert an image stream to a grayscale PNG stream
    static MemoryStream ConvertToGrayscale(Stream input)
    {
        // Load original image from the input stream
        using (Bitmap original = new Bitmap(input))
        {
            // Create a new bitmap for the grayscale image
            Bitmap grayBmp = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(grayBmp))
            {
                // Grayscale color matrix
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0,      0,      0,      1, 0},
                    new float[] {0,      0,      0,      0, 1}
                });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                // Draw the original image using the grayscale matrix
                g.DrawImage(
                    original,
                    new System.Drawing.Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }

            // Save the grayscale bitmap to a memory stream (PNG format)
            MemoryStream ms = new MemoryStream();
            grayBmp.Save(ms, ImageFormat.Png);
            ms.Position = 0; // reset for reading
            return ms;
        }
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (Aspose.Pdf rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Process only even pages
                if (pageIndex % 2 != 0) continue;

                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // XImageCollection is also 1‑based
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    XImage img = images[imgIndex];

                    // Save the original XImage to a temporary stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        img.Save(originalStream);
                        originalStream.Position = 0;

                        // Convert to grayscale
                        using (MemoryStream grayStream = ConvertToGrayscale(originalStream))
                        {
                            // Replace the image in the collection with the grayscale version
                            images.Replace(imgIndex, grayStream);
                        }
                    }
                }
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
