using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithQrPlaceholder
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string qrPlaceholderPath = "qr_placeholder.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(qrPlaceholderPath))
        {
            Console.Error.WriteLine($"QR placeholder image not found: {qrPlaceholderPath}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Read the placeholder image once into memory
            byte[] placeholderBytes = File.ReadAllBytes(qrPlaceholderPath);

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Process only odd‑numbered pages
                if (pageIndex % 2 == 0) continue;

                Page page = doc.Pages[pageIndex];

                // Find all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Replace each found image with the QR placeholder
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Use a fresh stream for each replacement
                    using (MemoryStream placeholderStream = new MemoryStream(placeholderBytes))
                    {
                        imgPlacement.Replace(placeholderStream);
                    }
                }
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPdfPath}");
    }
}