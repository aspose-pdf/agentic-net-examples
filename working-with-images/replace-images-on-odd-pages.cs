using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImagePlacementAbsorber, ImagePlacement

class ReplaceImagesOnOddPages
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string placeholderImage = "placeholder.jpg"; // JPEG placeholder

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(placeholderImage))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImage}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Open the placeholder image once; reset its position before each use
            using (FileStream placeholderStream = File.OpenRead(placeholderImage))
            {
                // Iterate over odd‑numbered pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex += 2)
                {
                    Page page = doc.Pages[pageIndex];

                    // Find all image placements on the current page
                    ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                    page.Accept(absorber);

                    // Replace each image with the placeholder while keeping its rectangle
                    foreach (ImagePlacement placement in absorber.ImagePlacements)
                    {
                        placeholderStream.Position = 0;               // rewind stream
                        placement.Replace(placeholderStream);          // replace image resource
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images on odd pages replaced and saved to '{outputPdf}'.");
    }
}
