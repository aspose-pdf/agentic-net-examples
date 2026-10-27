using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                    // Render the current page to a PNG stream first
                    using (MemoryStream pngStream = new MemoryStream())
                    {
                        var pngDevice = new PngDevice(new Resolution(300))
                        {
                            TransparentBackground = true
                        };
                        pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                        pngStream.Position = 0;

                        // Load the rendered PNG into a Bitmap
                        using (Bitmap originalBmp = new Bitmap(pngStream))
                        {
                            // Apply Bradley contrast‑enhancement (binarization) algorithm
                            using (Bitmap enhancedBmp = ApplyBradleyBinarization(originalBmp, 0.5))
                            {
                                // Save the enhanced bitmap as PNG
                                enhancedBmp.Save(outPath, ImageFormat.Png);
                            }
                        }
                    }

                    Console.WriteLine($"Page {pageNumber} saved as PNG: {outPath}");
                }
            }
        }
        // HTML‑to‑image conversions rely on GDI+, which is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("PDF‑to‑PNG conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    /// <summary>
    /// Applies the Bradley binarization (local contrast enhancement) algorithm to a bitmap.
    /// </summary>
    /// <param name="source">Source bitmap (any pixel format).</param>
    /// <param name="threshold">Threshold factor between 0.0 and 1.0 (commonly 0.5).</param>
    /// <returns>A new binarized bitmap.</returns>
    private static Bitmap ApplyBradleyBinarization(Bitmap source, double threshold)
    {
        int width = source.Width;
        int height = source.Height;

        // Convert source to 8‑bpp grayscale for easier processing
        Bitmap grayBmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
        // Set grayscale palette
        ColorPalette palette = grayBmp.Palette;
        for (int i = 0; i < 256; i++)
            palette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
        grayBmp.Palette = palette;

        // Populate grayscale bitmap using GetPixel (no unsafe code)
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                System.Drawing.Color pixel = source.GetPixel(x, y);
                byte gray = (byte)((pixel.R * 0.299) + (pixel.G * 0.587) + (pixel.B * 0.114));
                grayBmp.SetPixel(x, y, System.Drawing.Color.FromArgb(gray, gray, gray));
            }
        }

        // Build integral image for fast local sum calculation
        long[,] integral = new long[height + 1, width + 1];
        for (int y = 1; y <= height; y++)
        {
            long rowSum = 0;
            for (int x = 1; x <= width; x++)
            {
                byte gray = grayBmp.GetPixel(x - 1, y - 1).R;
                rowSum += gray;
                integral[y, x] = integral[y - 1, x] + rowSum;
            }
        }

        // Determine window size – typical choice is width/8
        int windowSize = Math.Max(1, width / 8);
        int half = windowSize / 2;
        long windowArea = (long)windowSize * windowSize;

        // Result bitmap – use 24‑bpp for easy SetPixel
        Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int x1 = Math.Max(0, x - half);
                int x2 = Math.Min(width - 1, x + half);
                int y1 = Math.Max(0, y - half);
                int y2 = Math.Min(height - 1, y + half);

                long sum = integral[y2 + 1, x2 + 1] - integral[y1, x2 + 1] - integral[y2 + 1, x1] + integral[y1, x1];
                byte current = grayBmp.GetPixel(x, y).R;
                bool isBlack = current * windowArea < sum * (1.0 - threshold);
                result.SetPixel(x, y, isBlack ? System.Drawing.Color.Black : System.Drawing.Color.White);
            }
        }

        grayBmp.Dispose();
        return result;
    }
}
