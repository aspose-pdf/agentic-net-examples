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
        const string inputPdf = "input.pdf";
        const string outputSprite = "sprite.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract images from the PDF using PdfExtractor
        List<Bitmap> extractedBitmaps = new List<Bitmap>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Use the mode that extracts actually used images (optional)
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // Save each image as PNG to the memory stream
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0;
                    // Load the image into a Bitmap for composition
                    Bitmap bmp = new Bitmap(imgStream);
                    extractedBitmaps.Add(bmp);
                }
            }
        }

        if (extractedBitmaps.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // Calculate sprite sheet dimensions (horizontal layout)
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (Bitmap bmp in extractedBitmaps)
        {
            totalWidth += bmp.Width;
            if (bmp.Height > maxHeight)
                maxHeight = bmp.Height;
        }

        // Create the sprite sheet bitmap
        using (Bitmap sprite = new Bitmap(totalWidth, maxHeight))
        using (Graphics g = Graphics.FromImage(sprite))
        {
            g.Clear(Color.Transparent);

            int offsetX = 0;
            foreach (Bitmap bmp in extractedBitmaps)
            {
                g.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                offsetX += bmp.Width;
                bmp.Dispose(); // Dispose individual bitmaps after drawing
            }

            // Save the combined sprite sheet as PNG
            sprite.Save(outputSprite, ImageFormat.Png);
        }

        Console.WriteLine($"Sprite sheet created: {outputSprite}");
    }
}