using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    // Simple heuristic: always treat the image as needing conversion.
    // Replace with a real palette‑checking implementation if required.
    static bool ContainsTargetPalette(XImage img)
    {
        // Placeholder – assume every image may contain the unwanted palette.
        return true;
    }

    // Convert the supplied XImage to a grayscale image and return the image data as a stream.
    static MemoryStream GetGrayscaleStream(XImage img)
    {
        // Save the original XImage into a memory stream.
        var originalStream = new MemoryStream();
        img.Save(originalStream);
        originalStream.Position = 0;

        // Load the image with System.Drawing so we can manipulate its pixels.
        using var bitmap = new Bitmap(originalStream);
        var grayBitmap = new Bitmap(bitmap.Width, bitmap.Height);

        using (var graphics = Graphics.FromImage(grayBitmap))
        {
            // Standard luminance conversion matrix.
            var colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                new float[] {0,      0,      0,      1, 0},
                new float[] {0,      0,      0,      0, 1}
            });
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            graphics.DrawImage(
                bitmap,
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), // Fully qualified to avoid ambiguity
                0, 0, bitmap.Width, bitmap.Height,
                GraphicsUnit.Pixel,
                attributes);
        }

        // Save the grayscale bitmap back to a stream (PNG is a safe choice).
        var grayStream = new MemoryStream();
        grayBitmap.Save(grayStream, ImageFormat.Png);
        grayStream.Position = 0;
        return grayStream;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_monochrome.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                // Access only the Images collection – no Shadings reference.
                XImageCollection images = page.Resources.Images;

                // Iterate through images using 1‑based index (required for Replace)
                for (int i = 1; i <= images.Count; i++)
                {
                    XImage img = images[i];

                    if (ContainsTargetPalette(img))
                    {
                        // Obtain a grayscale version of the image as a stream.
                        using MemoryStream grayStream = GetGrayscaleStream(img);

                        // Replace the original image with the grayscale image.
                        // XImageCollection.Replace expects a Stream, not an XImage.
                        images.Replace(i, grayStream);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}
