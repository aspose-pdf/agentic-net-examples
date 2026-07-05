using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf.Facades;

class SpriteSheetCreator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputSpritePath = "sprite.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // List to hold each extracted image as a Bitmap
        List<Bitmap> images = new List<Bitmap>();

        // Extract images from the PDF using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage(); // extract all images defined in resources

            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // Save each image as PNG into the memory stream
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0; // reset stream position for reading

                    // Load the image into a Bitmap (System.Drawing)
                    Bitmap bmp = new Bitmap(imgStream);
                    images.Add(bmp);
                }
            }
        }

        if (images.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // Calculate sprite sheet dimensions (horizontal layout)
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (Bitmap bmp in images)
        {
            totalWidth += bmp.Width;
            if (bmp.Height > maxHeight)
                maxHeight = bmp.Height;
        }

        // Create the sprite sheet bitmap
        using (Bitmap sprite = new Bitmap(totalWidth, maxHeight))
        using (Graphics g = Graphics.FromImage(sprite))
        {
            g.Clear(Color.Transparent); // start with a transparent background

            // Draw each image side by side
            int offsetX = 0;
            foreach (Bitmap bmp in images)
            {
                g.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                offsetX += bmp.Width;
                bmp.Dispose(); // free individual bitmap after drawing
            }

            // Save the combined sprite sheet as PNG
            sprite.Save(outputSpritePath, ImageFormat.Png);
        }

        Console.WriteLine($"Sprite sheet created: {outputSpritePath}");
    }
}