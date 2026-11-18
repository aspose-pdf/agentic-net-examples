using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Aspose.Pdf;

namespace PdfImageResizer
{
    // Represents the target dimensions for a page (in points, 1 point = 1/72 inch).
    public class PageImageSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    class Program
    {
        // DPI used for converting points to pixels when resizing with System.Drawing.
        private const float TargetDpi = 96f; // typical screen DPI; adjust if needed.

        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output_resized.pdf";
            const string configFilePath = "config.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(configFilePath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
                return;
            }

            // Load configuration: a JSON object where the key is the page number (1‑based)
            // and the value contains Width and Height (points).
            Dictionary<int, PageImageSize> pageSizes;
            try
            {
                string json = File.ReadAllText(configFilePath);
                pageSizes = JsonSerializer.Deserialize<Dictionary<int, PageImageSize>>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            using (Document doc = new Document(inputPdfPath))
            {
                // Iterate pages (Aspose.Pdf uses 1‑based indexing).
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    if (!pageSizes.TryGetValue(pageIndex, out PageImageSize targetSize))
                        continue; // No resizing defined for this page.

                    Page page = doc.Pages[pageIndex];
                    var images = page.Resources.Images;

                    // If the page contains no images, skip.
                    if (images == null || images.Count == 0)
                        continue;

                    // XImageCollection is 1‑based as well.
                    for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                    {
                        XImage xImg = images[imgIdx];

                        // Extract the original image into a memory stream.
                        using (MemoryStream originalStream = new MemoryStream())
                        {
                            xImg.Save(originalStream);
                            originalStream.Position = 0;

                            // Load with System.Drawing for resizing.
                            using (System.Drawing.Image original = System.Drawing.Image.FromStream(originalStream))
                            {
                                // Convert target size from points to pixels.
                                int targetPixelWidth = (int)Math.Round(targetSize.Width * TargetDpi / 72.0);
                                int targetPixelHeight = (int)Math.Round(targetSize.Height * TargetDpi / 72.0);

                                // Guard against zero dimensions.
                                if (targetPixelWidth <= 0) targetPixelWidth = original.Width;
                                if (targetPixelHeight <= 0) targetPixelHeight = original.Height;

                                using (Bitmap resized = new Bitmap(targetPixelWidth, targetPixelHeight))
                                {
                                    resized.SetResolution(TargetDpi, TargetDpi);
                                    using (Graphics g = Graphics.FromImage(resized))
                                    {
                                        g.CompositingQuality = CompositingQuality.HighQuality;
                                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        g.SmoothingMode = SmoothingMode.HighQuality;
                                        g.DrawImage(original, 0, 0, targetPixelWidth, targetPixelHeight);
                                    }

                                    // Save the resized bitmap to a new stream (preserve PNG format for lossless result).
                                    using (MemoryStream resizedStream = new MemoryStream())
                                    {
                                        // Preserve original format when possible.
                                        ImageFormat fmt = original.RawFormat;
                                        // Fallback to PNG if format is unknown.
                                        if (fmt.Equals(ImageFormat.MemoryBmp) || fmt.Equals(ImageFormat.Wmf) || fmt.Equals(ImageFormat.Emf))
                                            fmt = ImageFormat.Png;

                                        resized.Save(resizedStream, fmt);
                                        resizedStream.Position = 0;

                                        // Replace the image in the PDF with the resized version.
                                        images.Replace(imgIdx, resizedStream);
                                    }
                                }
                            }
                        }
                    }
                }

                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
        }
    }
}
