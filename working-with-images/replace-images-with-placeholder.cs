using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ReplaceImagesWithPlaceholders
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImgPath = "placeholder.png"; // small placeholder image
        const string placeholderUrl = "https://example.com/original-image.jpg"; // URL to original image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Absorb image placements to obtain rectangle information
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Process each image placement on the page
                foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
                {
                    // Find the index of the XImage in the page's image collection (1‑based)
                    int imgIndex = 1;
                    XImage targetImage = null;
                    foreach (XImage img in page.Resources.Images)
                    {
                        if (ReferenceEquals(img, placement.Image))
                        {
                            targetImage = img;
                            break;
                        }
                        imgIndex++;
                    }

                    if (targetImage == null)
                        continue; // safety check

                    // Replace the image with the placeholder (using XImageCollection.Replace)
                    using (FileStream placeholderStream = File.OpenRead(placeholderImgPath))
                    {
                        page.Resources.Images.Replace(imgIndex, placeholderStream);
                    }

                    // Add a hyperlink annotation over the original image rectangle
                    // Fully qualify Rectangle to avoid ambiguity
                    Aspose.Pdf.Rectangle linkRect = placement.Rectangle;
                    LinkAnnotation link = new LinkAnnotation(page, linkRect);
                    link.Action = new GoToURIAction(placeholderUrl);
                    link.Color = Aspose.Pdf.Color.Blue;
                    // Border must be created after the annotation instance exists
                    link.Border = new Border(link) { Width = 1 };

                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF (save without explicit SaveOptions writes PDF)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPdfPath}");
    }
}
