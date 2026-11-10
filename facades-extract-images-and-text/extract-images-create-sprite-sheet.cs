using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputSpritePath = "sprite_sheet.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Temporary folder for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        var extractedImageFiles = new List<string>();

        try
        {
            // ---------- Extract images using Aspose.Pdf.Facades ----------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 0;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset stream position

                    // Load the image and re‑save it as PNG (ensures uniform format)
                    using (Image img = Image.FromStream(imgStream))
                    {
                        string imgPath = Path.Combine(tempDir, $"img_{imageIndex++}.png");
                        img.Save(imgPath, ImageFormat.Png);
                        extractedImageFiles.Add(imgPath);
                    }
                }
            }

            if (extractedImageFiles.Count == 0)
            {
                Console.WriteLine("No images were found in the PDF.");
                return;
            }

            // ---------- Build sprite sheet ----------
            // Compute total width (horizontal layout) and max height
            int totalWidth = 0;
            int maxHeight  = 0;
            var loadedImages = new List<Image>();

            foreach (string file in extractedImageFiles)
            {
                Image img = Image.FromFile(file);
                loadedImages.Add(img);
                totalWidth += img.Width;
                if (img.Height > maxHeight) maxHeight = img.Height;
            }

            using (Bitmap sprite = new Bitmap(totalWidth, maxHeight))
            using (Graphics g = Graphics.FromImage(sprite))
            {
                g.Clear(Color.Transparent); // use transparent background

                int offsetX = 0;
                foreach (Image img in loadedImages)
                {
                    g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                    offsetX += img.Width;
                    img.Dispose(); // release each image after drawing
                }

                sprite.Save(outputSpritePath, ImageFormat.Png);
            }

            Console.WriteLine($"Sprite sheet created: {outputSpritePath}");
        }
        finally
        {
            // Clean up temporary files
            if (Directory.Exists(tempDir))
            {
                try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}