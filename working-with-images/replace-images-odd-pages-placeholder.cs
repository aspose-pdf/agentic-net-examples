using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImagePlacementAbsorber and ImagePlacement are defined here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImgPath = "placeholder.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                Page page = doc.Pages[pageNum];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Replace each found image with the placeholder while keeping its rectangle
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Open a fresh stream for each replacement (Replace reads the stream)
                    using (FileStream placeholderStream = File.OpenRead(placeholderImgPath))
                    {
                        imgPlacement.Replace(placeholderStream);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders on odd pages: {outputPdfPath}");
    }
}
