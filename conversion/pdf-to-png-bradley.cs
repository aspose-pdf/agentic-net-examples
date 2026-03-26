using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";
        const double threshold = 0.5; // value between 0.0 and 1.0 (Bradley threshold)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (Document pdfDoc = new Document(inputPdf))
        {
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Render the page to a PNG stream
                using (MemoryStream pngStream = new MemoryStream())
                {
                    var pngDevice = new Aspose.Pdf.Devices.PngDevice();
                    pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    pngStream.Position = 0;

                    // Load the PNG into a Bitmap
                    using (Bitmap originalBmp = new Bitmap(pngStream))
                    {
                        // Apply Bradley binarization
                        using (Bitmap binarizedBmp = ApplyBradleyBinarization(originalBmp, threshold))
                        {
                            string outPath = Path.Combine(outputFolder, $"page_{pageNum}.png");
                            binarizedBmp.Save(outPath, ImageFormat.Png);
                            Console.WriteLine($"Saved binarized PNG: {outPath}");
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Performs Bradley adaptive thresholding (local binarization) on a bitmap.
    /// </summary>
    /// <param name="source">Source bitmap (any pixel format).</param>
    /// <param name="threshold">Threshold factor between 0.0 and 1.0 (commonly 0.5).</param>
    /// <returns>A new binarized bitmap (1‑bpp indexed with black/white).</returns>
    private static Bitmap ApplyBradleyBinarization(Bitmap source, double threshold)
    {
        int width = source.Width;
        int height = source.Height;

        // ------------------------------------------------------------
        // 1. Convert source bitmap to an 8‑bpp grayscale bitmap.
        // ------------------------------------------------------------
        Bitmap gray = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
        // Set grayscale palette
        ColorPalette palette = gray.Palette;
        for (int i = 0; i < 256; i++)
        {
            palette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
        }
        gray.Palette = palette;

        // Fill the grayscale bitmap pixel‑by‑pixel using GetPixel/SetPixel (no unsafe code).
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                System.Drawing.Color c = source.GetPixel(x, y);
                // Standard luminance conversion
                byte lum = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                gray.SetPixel(x, y, System.Drawing.Color.FromArgb(lum, lum, lum));
            }
        }

        // ------------------------------------------------------------
        // 2. Build integral image for fast local sum calculation.
        // ------------------------------------------------------------
        long[,] integral = new long[height + 1, width + 1];
        for (int y = 1; y <= height; y++)
        {
            long rowSum = 0;
            for (int x = 1; x <= width; x++)
            {
                byte pixel = gray.GetPixel(x - 1, y - 1).R;
                rowSum += pixel;
                integral[y, x] = integral[y - 1, x] + rowSum;
            }
        }

        // ------------------------------------------------------------
        // 3. Apply Bradley adaptive thresholding.
        // ------------------------------------------------------------
        int windowSize = Math.Max(1, width / 8);
        int half = windowSize / 2;

        Bitmap result = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int x1 = Math.Max(x - half, 0);
                int y1 = Math.Max(y - half, 0);
                int x2 = Math.Min(x + half, width - 1);
                int y2 = Math.Min(y + half, height - 1);

                int area = (x2 - x1 + 1) * (y2 - y1 + 1);
                long sum = integral[y2 + 1, x2 + 1] - integral[y1, x2 + 1] - integral[y2 + 1, x1] + integral[y1, x1];

                byte pixel = gray.GetPixel(x, y).R;
                bool isBlack = pixel * area < sum * (1.0 - threshold);
                result.SetPixel(x, y, isBlack ? System.Drawing.Color.Black : System.Drawing.Color.White);
            }
        }

        gray.Dispose();
        return result;
    }
}
