using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string placeholderPath = "placeholder.png"; // a small PNG to use as a placeholder

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Find all image placements on the current page
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Iterate over each image placement
                foreach (ImagePlacement imgPlacement in imgAbsorber.ImagePlacements)
                {
                    // Determine the index of the XImage resource in the page's image collection (1‑based)
                    int imgIndex = 0;
                    int counter = 1;
                    foreach (XImage imgRes in page.Resources.Images)
                    {
                        if (imgRes == imgPlacement.Image)
                        {
                            imgIndex = counter;
                            break;
                        }
                        counter++;
                    }

                    if (imgIndex == 0)
                        continue; // image not found in the collection; skip

                    // Replace the original image with the placeholder image
                    using (FileStream placeholderStream = File.OpenRead(placeholderPath))
                    {
                        page.Resources.Images.Replace(imgIndex, placeholderStream);
                    }

                    // Build a hyperlink that points to the original image location.
                    // For demonstration we construct a URL using the page number and image index.
                    string originalImageUrl = $"https://example.com/original-image/page{pageNum}/image{imgIndex}.png";

                    // Create a link annotation over the same rectangle as the original image
                    LinkAnnotation link = new LinkAnnotation(page, imgPlacement.Rectangle);
                    link.Action = new GoToURIAction(originalImageUrl);
                    link.Color = Aspose.Pdf.Color.Blue; // optional visual cue
                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF (lifecycle rule: use the provided save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPath}");
    }
}