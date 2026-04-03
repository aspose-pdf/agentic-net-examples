using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Open the new logo once; reuse its stream for each replacement
            using (FileStream logoStream = File.OpenRead(logoPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Search for image placements on the current page
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    page.Accept(absorber);

                    // Replace each found image with the new logo
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        // Ensure the stream is positioned at the beginning for each replace call
                        logoStream.Position = 0;
                        placement.Replace(logoStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
    }
}