using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Absorb all image placements in the document
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            doc.Pages.Accept(imgAbsorber);

            // Iterate over each image placement
            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                XImage xImg = placement.Image;
                if (xImg == null)
                    continue;

                // Calculate DPI using the image's pixel size and the placement size (in points).
                // 1 point = 1/72 inch.
                double dpiX = 0, dpiY = 0;
                if (placement.Rectangle != null &&
                    placement.Rectangle.Width > 0 && placement.Rectangle.Height > 0)
                {
                    dpiX = (xImg.Width * 72.0) / placement.Rectangle.Width;
                    dpiY = (xImg.Height * 72.0) / placement.Rectangle.Height;
                }

                // If DPI information could not be derived, skip this image.
                if (dpiX == 0 && dpiY == 0)
                    continue;

                // Check if either horizontal or vertical DPI is below the threshold
                if (dpiX < 72 || dpiY < 72)
                {
                    // Find the page that contains this image (fallback when PageNumber is unavailable)
                    Page targetPage = null;
                    foreach (Page p in doc.Pages)
                    {
                        if (p.Resources != null && p.Resources.Images != null && p.Resources.Images.Contains(xImg))
                        {
                            targetPage = p;
                            break;
                        }
                    }

                    if (targetPage != null && targetPage.Resources != null && targetPage.Resources.Images != null)
                    {
                        targetPage.Resources.Images.Remove(xImg);
                        Console.WriteLine($"Removed image on page {targetPage.Number} with DPI {dpiX:F1}x{dpiY:F1}");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
