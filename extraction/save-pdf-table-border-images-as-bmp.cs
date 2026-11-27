using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TableBorderImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 0;

                // Iterate over all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;

                    // Save the XImage to a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);
                        ms.Position = 0;

                        // Load the stream into a System.Drawing.Bitmap
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            string outPath = Path.Combine(outputDir, $"page_{pageNum}_img_{imageIndex}.bmp");
                            bmp.Save(outPath, ImageFormat.Bmp);
                            Console.WriteLine($"Saved BMP: {outPath}");
                        }
                    }
                }
            }
        }
    }
}