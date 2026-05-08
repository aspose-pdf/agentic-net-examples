using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string placeholderPath = "placeholder.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderPath}");
            return;
        }

        // Load placeholder image into memory once; it will be reused for each replacement.
        byte[] placeholderBytes = File.ReadAllBytes(placeholderPath);

        // Load the PDF document (lifecycle: load → modify → save).
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages.
                if (pageNum % 2 == 0) continue;

                Page page = doc.Pages[pageNum];

                // Find all image placements on the current page.
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Replace each image with the placeholder while keeping its rectangle.
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Each Replace call consumes the stream, so provide a fresh one.
                    using (MemoryStream ms = new MemoryStream(placeholderBytes))
                    {
                        placement.Replace(ms);
                    }
                }
            }

            // Save the modified PDF (lifecycle: save).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images on odd pages replaced. Output saved to '{outputPath}'.");
    }
}