using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "region.png";
        const int pageNumber = 1;
        // Aspose.Pdf.Rectangle in PDF points (lower‑left x/y, upper‑right x/y)
        double llx = 100; // left
        double lly = 200; // bottom
        double urx = 300; // right
        double ury = 400; // top

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Set desired image resolution (DPI)
            const int dpi = 300; // Aspose.Pdf.Devices.Resolution expects integer DPI values
            Resolution resolution = new Resolution(dpi);
            PngDevice pngDevice = new PngDevice(resolution);

            // Convert the whole page to a PNG stored in memory
            using (MemoryStream pageStream = new MemoryStream())
            {
                pngDevice.Process(page, pageStream);
                pageStream.Position = 0;

                using (System.Drawing.Image fullImage = System.Drawing.Image.FromStream(pageStream))
                {
                    // PDF coordinates are in points (1/72 inch). Convert to pixels.
                    double scale = dpi / 72.0; // 1 point = 1/72 inch
                    // Image origin is top‑left, PDF origin is bottom‑left.
                    int cropX = (int)(llx * scale);
                    int cropY = (int)((page.Rect.Height - ury) * scale);
                    int cropWidth = (int)((urx - llx) * scale);
                    int cropHeight = (int)((ury - lly) * scale);

                    System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(cropX, cropY, cropWidth, cropHeight);

                    using (Bitmap bmp = new Bitmap(srcRect.Width, srcRect.Height))
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(fullImage, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), srcRect, GraphicsUnit.Pixel);
                        bmp.Save(outputPng);
                    }
                }
            }
        }

        Console.WriteLine($"Region saved to '{outputPng}'.");
    }
}
