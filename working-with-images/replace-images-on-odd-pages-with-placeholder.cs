using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImagePlacementAbsorber and related types are here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string placeholderImg = "placeholder.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(placeholderImg))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImg}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                doc.Pages[pageNum].Accept(absorber);

                // Replace each found image with the placeholder while keeping its rectangle, rotation, etc.
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Open the placeholder image stream for each replacement
                    using (FileStream placeholderStream = File.OpenRead(placeholderImg))
                    {
                        placement.Replace(placeholderStream);
                    }
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images on odd pages replaced. Output saved to '{outputPdf}'.");
    }
}
