using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction
using Aspose.Pdf.Text;   // for any text handling if needed

class ReplaceImagesWithPlaceholders
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output_with_placeholders.pdf";
        const string placeholderImgPath = "placeholder.jpg"; // JPEG placeholder image

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Absorb image placements to obtain rectangle positions
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Build a list of image placements on this page
                foreach (ImagePlacement imgPlacement in imgAbsorber.ImagePlacements)
                {
                    // Determine the index of the XImage resource in the page's image collection
                    int imgIndex = 0;
                    foreach (XImage img in page.Resources.Images)
                    {
                        imgIndex++;
                        if (img == imgPlacement.Image)
                            break;
                    }

                    if (imgIndex == 0)
                        continue; // Image not found in collection (should not happen)

                    // Replace the original image with the placeholder image
                    using (FileStream placeholderStream = File.OpenRead(placeholderImgPath))
                    {
                        // XImageCollection.Replace expects a 1‑based index
                        page.Resources.Images.Replace(imgIndex, placeholderStream);
                    }

                    // Create a hyperlink that points to the original image location.
                    // For demonstration, we construct a URL using the page number and image index.
                    string originalImageUrl = $"https://example.com/images/page{pageNum}_img{imgIndex}.jpg";

                    // Create a link annotation over the image rectangle.
                    // Use fully qualified Rectangle to avoid ambiguity.
                    Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                        imgPlacement.Rectangle.LLX,
                        imgPlacement.Rectangle.LLY,
                        imgPlacement.Rectangle.URX,
                        imgPlacement.Rectangle.URY);

                    LinkAnnotation link = new LinkAnnotation(page, linkRect)
                    {
                        // Set a visible border color (optional)
                        Color = Aspose.Pdf.Color.Blue,
                        // Use GoToURIAction for external URLs (rule: Hyperlink property is not a string)
                        Action = new GoToURIAction(originalImageUrl)
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPdfPath}");
    }
}