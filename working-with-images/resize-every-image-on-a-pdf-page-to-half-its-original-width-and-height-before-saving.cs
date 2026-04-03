using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Absorb all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Save the original image resource to a memory stream (PNG format)
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // Use System.Drawing.Imaging.ImageFormat for the format enum
                        placement.Image.Save(originalStream, System.Drawing.Imaging.ImageFormat.Png);
                        originalStream.Position = 0; // reset stream position

                        // Load the image into System.Drawing for resizing
                        using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalStream))
                        {
                            // Compute half size (ensure at least 1 pixel)
                            int newWidth  = Math.Max(1, originalImage.Width  / 2);
                            int newHeight = Math.Max(1, originalImage.Height / 2);

                            // Create a resized bitmap
                            using (System.Drawing.Bitmap resizedBitmap = new System.Drawing.Bitmap(originalImage, newWidth, newHeight))
                            {
                                // Save the resized bitmap back to a memory stream (PNG)
                                using (MemoryStream resizedStream = new MemoryStream())
                                {
                                    resizedBitmap.Save(resizedStream, ImageFormat.Png);
                                    resizedStream.Position = 0; // reset stream position

                                    // Replace the image in the PDF with the resized version
                                    placement.Replace(resizedStream);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
