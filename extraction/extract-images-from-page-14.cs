using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 14 exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 14)
                {
                    Console.Error.WriteLine("The document has fewer than 14 pages.");
                    return;
                }

                // Create an absorber that extracts image placements from a page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Apply the absorber to page 14
                doc.Pages[14].Accept(absorber);

                int imageIndex = 1;
                // Iterate over each extracted image placement
                foreach (var placement in absorber.ImagePlacements)
                {
                    // The Image property returns an XImage (not Aspose.Pdf.Image)
                    XImage xImg = placement.Image;

                    // Build a filename that includes the page number and image index.
                    // For simplicity we save all images as PNG – this works for any image type.
                    string fileName = Path.Combine(
                        outputDir,
                        $"page14_image{imageIndex}.png");

                    // Save the XImage to a file via a stream (XImage.Save only accepts a Stream)
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }

                    Console.WriteLine($"Saved: {fileName}");
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
