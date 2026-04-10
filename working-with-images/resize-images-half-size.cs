using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Required for ImagePlacementAbsorber

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
            // Iterate through each page
            foreach (Page page in doc.Pages)
            {
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

                        // Load the image as a Bitmap
                        using (Bitmap originalBitmap = new Bitmap(originalStream))
                        {
                            // Calculate half size (ensure at least 1 pixel)
                            int newWidth  = Math.Max(1, originalBitmap.Width  / 2);
                            int newHeight = Math.Max(1, originalBitmap.Height / 2);

                            // Create a resized bitmap
                            using (Bitmap resizedBitmap = new Bitmap(originalBitmap, newWidth, newHeight))
                            {
                                // Save the resized bitmap to a new stream
                                using (MemoryStream resizedStream = new MemoryStream())
                                {
                                    resizedBitmap.Save(resizedStream, ImageFormat.Png);
                                    resizedStream.Position = 0;

                                    // Replace the image on the page with the resized version
                                    placement.Replace(resizedStream);
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