using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Vector;
using System.Drawing; // Required for cropping the rendered bitmap (Windows only)
using System.Drawing.Imaging; // For ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (page indexing is 1‑based)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // ------------------------------------------------------------
                // 1. Use GraphicsAbsorber to collect all graphic elements on the page
                // ------------------------------------------------------------
                using (GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber())
                {
                    graphicsAbsorber.Visit(page);

                    // Define a predicate that selects only image graphic elements.
                    // In Aspose.Pdf.Vector the image element type is Aspose.Pdf.Image.
                    Predicate<GraphicElement> imagePredicate = element => element is Aspose.Pdf.Image;

                    // Use SvgExtractor with the absorber and predicate to obtain SVG strings
                    // for the filtered image elements (optional – demonstrates the filter).
                    SvgExtractor extractor = new SvgExtractor();
                    string svgContent = extractor.Extract(graphicsAbsorber, imagePredicate, page);
                    // (svgContent can be saved if needed; not required for JPEG export)

                    // ------------------------------------------------------------
                    // 2. Use ImagePlacementAbsorber to locate actual image resources.
                    //    This gives us the image rectangle on the page, which we will
                    //    use to crop the rendered bitmap.
                    // ------------------------------------------------------------
                    ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                    imgAbsorber.Visit(page);

                    int imageCounter = 0;
                    foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
                    {
                        // The placement rectangle defines where the image appears on the page.
                        // Render the whole page to a bitmap first.
                        using (MemoryStream pageBmpStream = new MemoryStream())
                        {
                            // Render the page to a bitmap using JpegDevice (default quality).
                            JpegDevice jpegDevice = new JpegDevice();
                            jpegDevice.Process(page, pageBmpStream);
                            pageBmpStream.Position = 0;

                            // Load the bitmap for cropping.
                            using (Bitmap fullPageBmp = new Bitmap(pageBmpStream))
                            {
                                // Convert Aspose.Pdf rectangle to System.Drawing rectangle.
                                // Aspose.Pdf.Rectangle uses lower‑left origin; System.Drawing uses upper‑left.
                                // Adjust Y coordinate accordingly.
                                double llx = placement.Rectangle.LLX;
                                double lly = placement.Rectangle.LLY;
                                double urx = placement.Rectangle.URX;
                                double ury = placement.Rectangle.URY;

                                // Page height in points (1 point = 1/72 inch)
                                double pageHeight = page.PageInfo.Height;

                                // Compute cropping rectangle in pixel space.
                                // Assume 72 DPI for simplicity; adjust if a different resolution is used.
                                int dpi = 72;
                                int x      = (int)(llx * dpi / 72.0);
                                int y      = (int)((pageHeight - ury) * dpi / 72.0);
                                int width  = (int)((urx - llx) * dpi / 72.0);
                                int height = (int)((ury - lly) * dpi / 72.0);

                                // Ensure the rectangle is within bitmap bounds.
                                System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(
                                    Math.Max(0, x),
                                    Math.Max(0, y),
                                    Math.Min(width,  fullPageBmp.Width  - x),
                                    Math.Min(height, fullPageBmp.Height - y));

                                // Crop the image.
                                using (Bitmap cropped = fullPageBmp.Clone(cropRect, fullPageBmp.PixelFormat))
                                {
                                    // Save the cropped bitmap as JPEG.
                                    string jpegPath = Path.Combine(
                                        outputDir,
                                        $"Page{pageIndex}_Image{++imageCounter}.jpg");

                                    cropped.Save(jpegPath, ImageFormat.Jpeg);
                                    Console.WriteLine($"Saved image: {jpegPath}");
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
