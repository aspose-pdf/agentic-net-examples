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
        const string inputPdfPath = "input.pdf";
        const string outputSpritePath = "sprite_sheet.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // List to hold extracted images
        List<Image> extractedImages = new List<Image>();

        // Extract images using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset stream position

                    // Load the image into System.Drawing.Image
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

        // Determine sprite sheet dimensions (horizontal layout)
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (var img in extractedImages)
        {
            totalWidth += img.Width;
            if (img.Height > maxHeight)
                maxHeight = img.Height;
        }

        // Create the sprite sheet bitmap
        using (Bitmap spriteSheet = new Bitmap(totalWidth, maxHeight))
        {
            using (Graphics g = Graphics.FromImage(spriteSheet))
            {
                g.Clear(Color.Transparent); // clear background

                int offsetX = 0;
                foreach (var img in extractedImages)
                {
                    g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                    offsetX += img.Width;
                }
            }

            // Save the sprite sheet as PNG
            spriteSheet.Save(outputSpritePath, ImageFormat.Png);
        }

        // Dispose extracted images
        foreach (var img in extractedImages)
        {
            img.Dispose();
        }

        Console.WriteLine($"Sprite sheet created: {outputSpritePath}");
    }
}