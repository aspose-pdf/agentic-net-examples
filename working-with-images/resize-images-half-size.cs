using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for ImagePlacementAbsorber

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Retrieve the original image as a stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        placement.Image.Save(originalStream);
                        originalStream.Position = 0;

                        // Load the image into a Bitmap for scaling
                        using (Bitmap originalBitmap = new Bitmap(originalStream))
                        {
                            // Calculate half size (ensure at least 1 pixel)
                            int newWidth  = Math.Max(1, originalBitmap.Width  / 2);
                            int newHeight = Math.Max(1, originalBitmap.Height / 2);

                            // Create a scaled bitmap
                            using (Bitmap scaledBitmap = new Bitmap(originalBitmap, new Size(newWidth, newHeight)))
                            {
                                // Save the scaled bitmap to a new memory stream (PNG format)
                                using (MemoryStream scaledStream = new MemoryStream())
                                {
                                    scaledBitmap.Save(scaledStream, System.Drawing.Imaging.ImageFormat.Png);
                                    scaledStream.Position = 0;

                                    // Replace the image in the PDF with the scaled version
                                    placement.Replace(scaledStream);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}