using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
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

        List<Image> extractedImages = new List<Image>();

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0;
                    Image img = Image.FromStream(imageStream);
                    // Clone to detach from the stream
                    extractedImages.Add(new Bitmap(img));
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
        int maxHeight = 0;
        foreach (Image img in extractedImages)
        {
            totalWidth += img.Width;
            if (img.Height > maxHeight)
            {
                maxHeight = img.Height;
            }
        }

        using (Bitmap spriteBitmap = new Bitmap(totalWidth, maxHeight))
        {
            using (Graphics graphics = Graphics.FromImage(spriteBitmap))
            {
                graphics.Clear(Color.Transparent);
                int offsetX = 0;
                foreach (Image img in extractedImages)
                {
                    graphics.DrawImage(img, offsetX, 0, img.Width, img.Height);
                    offsetX += img.Width;
                    img.Dispose();
                }
            }
            spriteBitmap.Save(outputSpritePath, ImageFormat.Png);
        }

        Console.WriteLine($"Sprite sheet created: {outputSpritePath}");
    }
}
