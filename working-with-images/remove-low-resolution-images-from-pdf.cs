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

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and collect its image placements
            foreach (Page page in doc.Pages)
            {
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // DPI is available via the Resolution property (X and Y)
                    double dpiX = placement.Resolution.X;
                    double dpiY = placement.Resolution.Y;

                    if (dpiX < 72 || dpiY < 72)
                    {
                        // Remove the low‑DPI image from the current page
                        // ImagePlacement.Image returns an Aspose.Pdf.Image (XImage) whose Name can be used for deletion
                        var img = placement.Image;
                        if (img != null && !string.IsNullOrEmpty(img.Name))
                        {
                            page.Resources.Images.Delete(img.Name);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Low‑DPI images removed. Saved to '{outputPath}'.");
    }
}
