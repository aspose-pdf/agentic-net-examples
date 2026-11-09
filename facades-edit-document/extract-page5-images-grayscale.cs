using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -------------------------------------------------------------------
            // NOTE: In recent versions of Aspose.PDF the "ImageExtractor" class has
            // been removed.  The recommended way to obtain embedded images is to
            // use the ImagePlacementAbsorber class which walks the page content and
            // collects all image placements.
            // -------------------------------------------------------------------
            // Create an absorber for page 5 (pages are 1‑based)
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            absorber.Visit(pdfDoc.Pages[5]);

            var imagePlacements = absorber.ImagePlacements;
            int imageCount = imagePlacements.Count;

            for (int i = 0; i < imageCount; i++)
            {
                // Each placement contains an XImage instance representing the
                // embedded image.
                XImage xImage = imagePlacements[i].Image;

                // Build a unique file name for each image
                string outputPath = Path.Combine(outputFolder, $"Page5_Image{i + 1}.jpg");

                // Save the XImage to a memory stream first (original format)
                using (MemoryStream originalStream = new MemoryStream())
                {
                    // XImage.Save can write directly to a stream using a .NET image format.
                    // We use PNG as an intermediate format because it preserves the
                    // original pixel data without compression artifacts.
                    xImage.Save(originalStream, ImageFormat.Png);
                    originalStream.Position = 0;

                    // Load the image into a System.Drawing.Bitmap so we can apply a
                    // grayscale color matrix.
                    using (Bitmap bitmap = new Bitmap(originalStream))
                    {
                        // Create a new bitmap that will hold the grayscale result.
                        using (Bitmap grayBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                        {
                            using (Graphics g = Graphics.FromImage(grayBitmap))
                            {
                                // Standard luminance conversion matrix.
                                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                {
                                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                                    new float[] {0,      0,      0,      1, 0},
                                    new float[] {0,      0,      0,      0, 1}
                                });

                                ImageAttributes attributes = new ImageAttributes();
                                attributes.SetColorMatrix(colorMatrix);

                                // Draw the original bitmap onto the new bitmap using the
                                // grayscale color matrix.
                                g.DrawImage(
                                    bitmap,
                                    new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), // Fully qualified to avoid CS0104
                                    0, 0, bitmap.Width, bitmap.Height,
                                    GraphicsUnit.Pixel,
                                    attributes);
                            }

                            // Finally, save the grayscale bitmap as JPEG.
                            grayBitmap.Save(outputPath, ImageFormat.Jpeg);
                        }
                    }
                }

                Console.WriteLine($"Saved grayscale JPEG: {outputPath}");
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
