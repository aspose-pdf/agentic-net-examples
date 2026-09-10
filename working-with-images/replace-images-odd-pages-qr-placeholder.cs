using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceImagesWithQrPlaceholder
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string qrPlaceholderPath = "qr_placeholder.png"; // QR code image file

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

        // Load the original PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Read QR placeholder bytes once – will be reused for each replacement
            byte[] qrBytes = File.ReadAllBytes(qrPlaceholderPath);

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                Page page = doc.Pages[pageNum];

                // Find all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // For each image placement, replace the underlying XImage with the QR placeholder
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Determine the index of the XImage within the page's image collection (1‑based)
                    int imgIndex = 1;
                    XImage targetImage = null;
                    foreach (XImage img in page.Resources.Images)
                    {
                        if (img == placement.Image)
                        {
                            targetImage = img;
                            break;
                        }
                        imgIndex++;
                    }

                    if (targetImage == null) continue; // Safety check

                    // Replace the image with the QR placeholder
                    using (MemoryStream qrStream = new MemoryStream(qrBytes))
                    {
                        page.Resources.Images.Replace(imgIndex, qrStream);
                    }

                    // After replacement, obtain the new XImage instance
                    XImage newImg = page.Resources.Images[imgIndex];

                    // Set alternative text that points to the original image source (placeholder URL)
                    string altText = $"Replaced image on page {pageNum}";
                    newImg.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPdfPath}");
    }
}