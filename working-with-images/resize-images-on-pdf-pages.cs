using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for ImagePlacementAbsorber (belongs to Aspose.Pdf namespace, but included for clarity)

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Extract the original image into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        placement.Image.Save(originalStream, ImageFormat.Png);
                        originalStream.Position = 0;

                        // Load the image as a Bitmap to perform scaling
                        using (Bitmap originalBitmap = new Bitmap(originalStream))
                        {
                            // Calculate half size (ensure at least 1 pixel)
                            int newWidth  = Math.Max(1, originalBitmap.Width  / 2);
                            int newHeight = Math.Max(1, originalBitmap.Height / 2);

                            // Create a scaled bitmap
                            using (Bitmap scaledBitmap = new Bitmap(originalBitmap, newWidth, newHeight))
                            {
                                // Save the scaled bitmap back to a stream
                                using (MemoryStream scaledStream = new MemoryStream())
                                {
                                    scaledBitmap.Save(scaledStream, ImageFormat.Png);
                                    scaledStream.Position = 0;

                                    // Replace the original image placement with the scaled image
                                    placement.Replace(scaledStream);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}