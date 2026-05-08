using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Devices;
using System.Drawing;               // For Bitmap, Rectangle, etc.
using System.Drawing.Imaging;      // For ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a GraphicsAbsorber and visit the current page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Predicate that selects only image graphic elements (Aspose.Pdf.Image).
                    Predicate<GraphicElement> imageFilter = element => element is Aspose.Pdf.Image;

                    // Render the whole page to a bitmap (high‑resolution JPEG device).
                    // The bitmap will be used for cropping each image region.
                    Resolution resolution = new Resolution(300); // 300 DPI for good quality
                    JpegDevice jpegDevice = new JpegDevice(resolution);

                    using (MemoryStream pageStream = new MemoryStream())
                    {
                        jpegDevice.Process(page, pageStream);
                        pageStream.Position = 0;
                        using (Bitmap fullPageBmp = new Bitmap(pageStream))
                        {
                            int imageCounter = 1;
                            foreach (GraphicElement element in absorber.Elements)
                            {
                                if (!imageFilter(element))
                                    continue; // Skip non‑image elements (e.g., paths)

                                // Try to obtain the element's bounding rectangle.
                                // Many graphic elements expose a "BoundingBox" property.
                                Aspose.Pdf.Rectangle bbox = null;
                                var bboxProp = element.GetType().GetProperty("BoundingBox");
                                if (bboxProp != null)
                                    bbox = bboxProp.GetValue(element) as Aspose.Pdf.Rectangle;

                                if (bbox == null)
                                    continue; // Cannot determine location; skip.

                                // Convert PDF coordinates (origin at lower‑left) to
                                // System.Drawing coordinates (origin at upper‑left).
                                int x = (int)bbox.LLX;
                                int y = (int)(page.PageInfo.Height - bbox.URY);
                                int width  = (int)(bbox.URX - bbox.LLX);
                                int height = (int)(bbox.URY - bbox.LLY);

                                // Ensure the rectangle is within the bitmap bounds.
                                var cropRect = new System.Drawing.Rectangle(
                                    Math.Max(0, x),
                                    Math.Max(0, y),
                                    Math.Min(width,  fullPageBmp.Width  - x),
                                    Math.Min(height, fullPageBmp.Height - y));

                                if (cropRect.Width <= 0 || cropRect.Height <= 0)
                                    continue; // Invalid region.

                                // Crop the image region.
                                using (Bitmap cropped = fullPageBmp.Clone(cropRect, fullPageBmp.PixelFormat))
                                {
                                    string outPath = Path.Combine(
                                        outputDir,
                                        $"Page{pageIndex}_Image{imageCounter}.jpeg");

                                    // Save the cropped bitmap as JPEG.
                                    cropped.Save(outPath, ImageFormat.Jpeg);
                                    Console.WriteLine($"Saved image: {outPath}");
                                    imageCounter++;
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
