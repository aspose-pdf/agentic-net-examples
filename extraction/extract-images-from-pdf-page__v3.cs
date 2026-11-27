using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for XImage.Save
using Aspose.Pdf;
using Aspose.Pdf.Text; // ImagePlacementAbsorber and related types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check that page 7 is present (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Apply ImagePlacementAbsorber to page 7
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            doc.Pages[7].Accept(absorber);

            int imageIndex = 1;
            // Iterate over all located images on the page
            foreach (var placement in absorber.ImagePlacements)
            {
                // The extracted image
                var img = placement.Image;

                // Build a unique file name for each image
                string outPath = System.IO.Path.Combine(outputDir, $"Page7_Image_{imageIndex}.png");

                // Save the image as PNG using a FileStream and System.Drawing.Imaging.ImageFormat
                using (FileStream fs = File.Create(outPath))
                {
                    img.Save(fs, ImageFormat.Png);
                }

                Console.WriteLine($"Saved image {imageIndex} to '{outPath}'.");
                imageIndex++;
            }
        }
    }
}
