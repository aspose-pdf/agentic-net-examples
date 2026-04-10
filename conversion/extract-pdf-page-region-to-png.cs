using System;
using System.IO;
using System.Drawing;                     // For Bitmap and Rectangle
using System.Drawing.Imaging;            // For ImageFormat
using Aspose.Pdf;                         // Core PDF API
using Aspose.Pdf.Devices;                 // Image devices (PngDevice, Resolution)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output PNG file path (region image)
        const string outputPng = "region.png";

        // Page number to extract (1‑based indexing)
        const int pageNumber = 1;

        // Define the rectangle region in PDF points (1 point = 1/72 inch)
        // Aspose.Pdf.Rectangle(llx, lly, urx, ury) – lower‑left and upper‑right corners
        // Adjust these values to the desired area.
        Aspose.Pdf.Rectangle pdfRegion = new Aspose.Pdf.Rectangle(
            llx: 100,   // left
            lly: 200,   // bottom
            urx: 300,   // right
            ury: 400    // top
        );

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using statement guarantees disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Create a resolution for the PNG image (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // PngDevice converts a PDF page to a PNG stream
            PngDevice pngDevice = new PngDevice(resolution);

            // Convert the whole page to a PNG stored in a memory stream
            using (MemoryStream fullPageStream = new MemoryStream())
            {
                pngDevice.Process(pdfDoc.Pages[pageNumber], fullPageStream);
                fullPageStream.Position = 0; // Reset stream position for reading

                // Load the PNG into a System.Drawing.Bitmap for cropping
                using (Bitmap fullBitmap = new Bitmap(fullPageStream))
                {
                    // PDF points to pixel conversion factor (points * DPI / 72)
                    double factorX = resolution.X / 72.0;
                    double factorY = resolution.Y / 72.0;

                    // Calculate pixel coordinates of the rectangle
                    int pixelX = (int)(pdfRegion.LLX * factorX);
                    int pixelY = (int)(fullBitmap.Height - (pdfRegion.URY * factorY)); // Y origin is top‑left in bitmap
                    int pixelWidth  = (int)(pdfRegion.Width  * factorX);
                    int pixelHeight = (int)(pdfRegion.Height * factorY);

                    // Ensure the crop rectangle is within the bitmap bounds
                    int safeX = Math.Max(0, pixelX);
                    int safeY = Math.Max(0, pixelY);
                    int safeWidth  = Math.Min(pixelWidth,  fullBitmap.Width  - safeX);
                    int safeHeight = Math.Min(pixelHeight, fullBitmap.Height - safeY);

                    System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(
                        safeX,
                        safeY,
                        safeWidth,
                        safeHeight);

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
