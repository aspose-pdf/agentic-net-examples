using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace as requested
using System.Drawing;               // For System.Drawing.Image (grayscale conversion)
using System.Drawing.Imaging;       // For ImageFormat

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Process only even pages
                if (pageIndex % 2 == 0)
                {
                    // Absorb image placements on the current page
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    doc.Pages[pageIndex].Accept(absorber);

                    // Replace each image with its grayscale version
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        // Original XImage resource
                        XImage originalXImage = placement.Image;

                        // Obtain a System.Drawing.Image that is a grayscale copy
                        System.Drawing.Image grayImage = originalXImage.Grayscaled;

                        // Write the grayscale image to a memory stream (PNG format)
                        using (MemoryStream grayStream = new MemoryStream())
                        {
                            grayImage.Save(grayStream, ImageFormat.Png);
                            grayStream.Position = 0; // Reset stream position for reading

                            // Replace the image resource in the PDF with the grayscale stream
                            placement.Replace(grayStream);
                        }

                        // Clean up the temporary System.Drawing.Image
                        grayImage.Dispose();
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}