using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";
        const double newWidth = 200;   // Desired width in points (treated as pixels for raster images)
        const double newHeight = 150;  // Desired height in points (treated as pixels for raster images)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // XImage collection is 1‑based, so we use a for‑loop to get the index
                    int imageCount = page.Resources.Images.Count;
                    for (int i = 1; i <= imageCount; i++)
                    {
                        XImage xImg = page.Resources.Images[i];

                        // Extract the original image bytes from the XImage
                        using (var originalBytes = new MemoryStream())
                        {
                            xImg.Save(originalBytes); // XImage.Save writes to a stream
                            originalBytes.Position = 0;

                            // Load the raster image with System.Drawing, resize it, and write back to a new stream
                            using (System.Drawing.Image original = System.Drawing.Image.FromStream(originalBytes))
                            {
                                int targetW = (int)Math.Round(newWidth);
                                int targetH = (int)Math.Round(newHeight);

                                using (var resizedBmp = new System.Drawing.Bitmap(targetW, targetH))
                                {
                                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(resizedBmp))
                                    {
                                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        g.DrawImage(original, 0, 0, targetW, targetH);
                                    }

                                    using (var newImgStream = new MemoryStream())
                                    {
                                        // Preserve the original format when possible; fallback to PNG
                                        ImageFormat fmt = original.RawFormat;
                                        if (fmt.Equals(ImageFormat.MemoryBmp) || fmt.Equals(ImageFormat.Wmf) || fmt.Equals(ImageFormat.Emf))
                                            fmt = ImageFormat.Png;

                                        resizedBmp.Save(newImgStream, fmt);
                                        newImgStream.Position = 0;

                                        // Replace the image in the PDF resources collection
                                        page.Resources.Images.Replace(i, newImgStream);
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
