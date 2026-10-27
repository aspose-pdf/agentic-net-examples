using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // NOTE: This line is intentionally omitted to respect the hard namespace restriction.

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and absorb its image placements
            foreach (Page page in doc.Pages)
            {
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each image placement on the current page
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // DPI information is available via the Resolution property
                    double dpiX = placement.Resolution.X;
                    double dpiY = placement.Resolution.Y;

                    // Delete images whose horizontal or vertical DPI is below 72
                    if (dpiX < 72 || dpiY < 72)
                    {
                        // The image object associated with the placement is an XImage
                        Aspose.Pdf.XImage xImg = placement.Image;

                        // Remove the image from the page's resources if it exists
                        if (page.Resources.Images.Contains(xImg))
                        {
                            page.Resources.Images.Remove(xImg);
                        }
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
