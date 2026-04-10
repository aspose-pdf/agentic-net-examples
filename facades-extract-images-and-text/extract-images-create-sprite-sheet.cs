using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPng = "sprite_sheet.png";   // combined image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // List to hold extracted images
        List<Image> extractedImages = new List<Image>();

        // Extract images from the PDF using PdfExtractor (Facade)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // The ExtractImageMode property does not exist in the referenced Aspose.Pdf version.
            // Extraction works with the default settings, so we simply call ExtractImage().
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // removed for compatibility
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream as PNG
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // GetNextImage overload with ImageFormat
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0; // reset for reading

                    // Load the image from the stream and keep it in the list
                    Image img = Image.FromStream(imgStream);
                    extractedImages.Add(img);
                }
            }
        }

        if (extractedImages.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // Calculate sprite sheet dimensions (horizontal layout)
        int totalWidth = 0;
        int maxHeight  = 0;
        foreach (var img in extractedImages)
        {
            totalWidth += img.Width;
            if (img.Height > maxHeight) maxHeight = img.Height;
        }

        // Create the sprite sheet bitmap
        using (Bitmap sprite = new Bitmap(totalWidth, maxHeight, PixelFormat.Format32bppArgb))
        using (Graphics g = Graphics.FromImage(sprite))
        {
            g.Clear(Color.Transparent); // optional: transparent background

            // Draw each image side by side
            int offsetX = 0;
            foreach (var img in extractedImages)
            {
                g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                offsetX += img.Width;
                img.Dispose(); // release individual image resources
            }

            // Save the combined sprite sheet as PNG
            sprite.Save(outputPng, ImageFormat.Png);
        }

        Console.WriteLine($"Sprite sheet created: {outputPng}");
    }
}
