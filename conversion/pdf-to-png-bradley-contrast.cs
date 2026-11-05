using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice, Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "png_output";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // 1. Convert each PDF page to a PNG image using Aspose.Pdf.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use PngDevice (core API) instead of ImageSaveOptions (facade‑style).
            var resolution = new Resolution(300);
            var pngDevice = new PngDevice(resolution);

            int pageNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                string outPath = Path.Combine(outputFolder, $"output_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(page, imageStream);
                }
                pageNumber++;
            }
        }

        // -----------------------------------------------------------------
        // 2. Apply a simple Bradley local contrast enhancement to each PNG.
        //    Implemented manually using System.Drawing to avoid the Aspose.Imaging
        //    dependency.
        // -----------------------------------------------------------------
        foreach (string pngPath in Directory.GetFiles(outputFolder, "*.png"))
        {
            using (Bitmap bitmap = new Bitmap(pngPath))
            {
                ApplyBradleyContrast(bitmap, 0.5f, 8);
                // Overwrite the original file with the enhanced image.
                bitmap.Save(pngPath, ImageFormat.Png);
            }
        }

        Console.WriteLine("PDF to PNG conversion completed with Bradley contrast enhancement.");
    }

    /// <summary>
    /// Applies a basic Bradley local contrast enhancement algorithm.
    /// </summary>
    /// <param name="src">Source bitmap (will be modified in‑place).</param>
    /// <param name="threshold">Threshold in the range (0‑1). Typical values: 0.5‑0.7.</param>
    /// <param name="windowSize">Half‑size of the square window used for local averaging.</param>
    private static void ApplyBradleyContrast(Bitmap src, float threshold, int windowSize)
    {
        int width = src.Width;
        int height = src.Height;
        // Build integral image for fast local mean calculation (grayscale).
        long[,] integral = new long[height + 1, width + 1];
        for (int y = 0; y < height; y++)
        {
            long rowSum = 0;
            for (int x = 0; x < width; x++)
            {
                System.Drawing.Color c = src.GetPixel(x, y);
                // Convert to luminance using Rec. 601 coefficients.
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                rowSum += lum;
                integral[y + 1, x + 1] = integral[y, x + 1] + rowSum;
            }
        }

        // Process each pixel.
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Define window bounds.
                int x1 = Math.Max(x - windowSize, 0);
                int y1 = Math.Max(y - windowSize, 0);
                int x2 = Math.Min(x + windowSize, width - 1);
                int y2 = Math.Min(y + windowSize, height - 1);

                int area = (x2 - x1 + 1) * (y2 - y1 + 1);
                long sum = integral[y2 + 1, x2 + 1] - integral[y1, x2 + 1] - integral[y2 + 1, x1] + integral[y1, x1];
                int localMean = (int)(sum / area);

                System.Drawing.Color orig = src.GetPixel(x, y);
                int lumOrig = (int)(0.299 * orig.R + 0.587 * orig.G + 0.114 * orig.B);
                // If pixel is darker than (threshold * localMean) make it black, else white.
                int newLum = (lumOrig < localMean * threshold) ? 0 : 255;
                System.Drawing.Color newColor = System.Drawing.Color.FromArgb(newLum, newLum, newLum);
                src.SetPixel(x, y, newColor);
            }
        }
    }
}
