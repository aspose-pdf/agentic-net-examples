using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_monochrome.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];

                // Collect images to process because we will modify the collection while iterating
                List<Aspose.Pdf.XImage> images = new List<Aspose.Pdf.XImage>();
                foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                {
                    images.Add(img);
                }

                foreach (Aspose.Pdf.XImage img in images)
                {
                    // Extract the original image into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        img.Save(originalStream);
                        originalStream.Position = 0;

                        // Load the image with System.Drawing for pixel manipulation
                        using (Bitmap bitmap = new Bitmap(originalStream))
                        {
                            // Create a grayscale bitmap
                            using (Bitmap grayBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                            {
                                using (Graphics g = Graphics.FromImage(grayBitmap))
                                {
                                    // Standard luminance conversion matrix
                                    ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                    {
                                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                                        new float[] {0, 0, 0, 1, 0},
                                        new float[] {0, 0, 0, 0, 1}
                                    });

                                    ImageAttributes attributes = new ImageAttributes();
                                    attributes.SetColorMatrix(colorMatrix);

                                    g.DrawImage(
                                        bitmap,
                                        new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                        0, 0, bitmap.Width, bitmap.Height,
                                        GraphicsUnit.Pixel,
                                        attributes);
                                }

                                // Save the grayscale image to a new memory stream (PNG format)
                                using (MemoryStream grayStream = new MemoryStream())
                                {
                                    grayBitmap.Save(grayStream, ImageFormat.Png);
                                    grayStream.Position = 0;

                                    // Add the new grayscale image to the page resources
                                    string grayImgName = page.Resources.Images.Add(grayStream);

                                    // OPTIONAL: replace the original image reference in the page content.
                                    // This simple example just removes the original image from the resources.
                                    // More sophisticated content replacement would require parsing the page content stream.
                                    page.Resources.Images.Remove(img);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Monochrome PDF saved to '{outputPath}'.");
    }
}
