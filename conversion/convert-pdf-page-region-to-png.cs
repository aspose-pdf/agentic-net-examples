using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "region.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Choose the page (1‑based index)
            Page page = pdfDoc.Pages[1];

            // Define the region in PDF points (1 point = 1/72 inch)
            // Example: rectangle from (100, 500) to (300, 700)
            Aspose.Pdf.Rectangle pdfRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Convert the whole page to PNG in a memory stream
            using (MemoryStream fullPngStream = page.ConvertToPNGMemoryStream())
            {
                // Load the PNG into a Bitmap for cropping
                using (Bitmap fullBitmap = new Bitmap(fullPngStream))
                {
                    // Determine the resolution used by the conversion (default 150 DPI)
                    const int resolution = 150;
                    // Convert PDF points to pixel coordinates
                    int left   = (int)Math.Round(pdfRect.LLX * resolution / 72.0);
                    int bottom = (int)Math.Round(pdfRect.LLY * resolution / 72.0);
                    int right  = (int)Math.Round(pdfRect.URX * resolution / 72.0);
                    int top    = (int)Math.Round(pdfRect.URY * resolution / 72.0);

                    // In GDI+ the origin (0,0) is top‑left, so we need to flip the Y axis
                    int height = fullBitmap.Height;
                    int cropY = height - top;
                    int cropHeight = top - bottom;
                    int cropX = left;
                    int cropWidth = right - left;

                    // Ensure the crop rectangle is within the bitmap bounds
                    System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(
                        Math.Max(cropX, 0),
                        Math.Max(cropY, 0),
                        Math.Min(cropWidth, fullBitmap.Width - Math.Max(cropX, 0)),
                        Math.Min(cropHeight, fullBitmap.Height - Math.Max(cropY, 0)));

                    // Crop the region
                    using (Bitmap regionBitmap = fullBitmap.Clone(cropRect, fullBitmap.PixelFormat))
                    {
                        // Save the cropped region as PNG
                        regionBitmap.Save(outputPng, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine($"Region PNG saved to '{outputPng}'.");
    }
}
