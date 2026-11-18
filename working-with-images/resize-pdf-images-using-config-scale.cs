using System;
using System.IO;
using System.Text.Json;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Load scaling factor from appsettings.json using System.Text.Json (no extra NuGet packages required)
        const string configPath = "appsettings.json";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        double scaleFactor;
        try
        {
            using var stream = File.OpenRead(configPath);
            using var doc = JsonDocument.Parse(stream);
            if (!doc.RootElement.TryGetProperty("ImageScaleFactor", out JsonElement factorElem) ||
                !factorElem.TryGetDouble(out scaleFactor))
            {
                Console.Error.WriteLine("'ImageScaleFactor' not found or not a valid number in appsettings.json.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_resized.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF and replace each image with a scaled version
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                var images = page.Resources.Images;

                // XImageCollection is 1‑based as well
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    XImage xImg = images[imgIndex];

                    // Export the original image to a memory stream
                    using var originalStream = new MemoryStream();
                    xImg.Save(originalStream);
                    originalStream.Position = 0;

                    // Load it into System.Drawing for resizing (fully qualified to avoid ambiguity)
                    using System.Drawing.Image originalBitmap = System.Drawing.Image.FromStream(originalStream);
                    int newWidth  = (int)(originalBitmap.Width  * scaleFactor);
                    int newHeight = (int)(originalBitmap.Height * scaleFactor);

                    // Create a resized bitmap
                    using var resizedBitmap = new Bitmap(originalBitmap, new Size(newWidth, newHeight));

                    // Save the resized bitmap back to a stream in the same format as the original
                    using var resizedStream = new MemoryStream();
                    ImageFormat format = GetImageFormat(originalBitmap.RawFormat);
                    resizedBitmap.Save(resizedStream, format);
                    resizedStream.Position = 0;

                    // Replace the image in the PDF resources
                    images.Replace(imgIndex, resizedStream);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized by factor {scaleFactor} and saved to '{outputPdfPath}'.");
    }

    // Helper to map raw format to a known ImageFormat (fallback to Png)
    private static ImageFormat GetImageFormat(ImageFormat rawFormat)
    {
        if (rawFormat.Equals(ImageFormat.Jpeg)) return ImageFormat.Jpeg;
        if (rawFormat.Equals(ImageFormat.Png))  return ImageFormat.Png;
        if (rawFormat.Equals(ImageFormat.Gif))  return ImageFormat.Gif;
        if (rawFormat.Equals(ImageFormat.Bmp))  return ImageFormat.Bmp;
        if (rawFormat.Equals(ImageFormat.Tiff)) return ImageFormat.Tiff;
        // Default fallback
        return ImageFormat.Png;
    }
}
