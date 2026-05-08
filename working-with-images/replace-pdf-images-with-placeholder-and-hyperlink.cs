using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ReplaceImagesWithPlaceholder
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string placeholderImgPath = "placeholder.png"; // low‑resolution placeholder image
        const string outputPdfPath = "output.pdf";

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

        // Load the original PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Read placeholder image into a byte array once (will be reused for each replacement)
            byte[] placeholderBytes = File.ReadAllBytes(placeholderImgPath);

            // Process each page
            foreach (Page page in doc.Pages)
            {
                // Find all image placements on the page (to obtain their rectangles)
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Iterate over each placed image
                foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
                {
                    // ------------------------------------------------------------
                    // 1. Replace the underlying XImage resource with the placeholder
                    // ------------------------------------------------------------
                    XImage originalImg = placement.Image;
                    int imgIndex = 0;
                    foreach (XImage x in page.Resources.Images)
                    {
                        imgIndex++;
                        if (ReferenceEquals(x, originalImg))
                            break;
                    }

                    // Replace using a fresh MemoryStream for each call (required by the API)
                    using (MemoryStream placeholderStream = new MemoryStream(placeholderBytes))
                    {
                        page.Resources.Images.Replace(imgIndex, placeholderStream);
                    }

                    // ------------------------------------------------------------
                    // 2. Add a hyperlink annotation that points to the original image URL
                    // ------------------------------------------------------------
                    // Build a reference URL – in a real scenario this could be derived from metadata.
                    // Here we simply use a placeholder URL that includes the page and image index.
                    string originalImageUrl = $"https://example.com/original-image/page{page.Number}_img{imgIndex}.png";

                    // Create a link annotation covering the image rectangle
                    Aspose.Pdf.Rectangle linkRect = placement.Rectangle; // rectangle of the image on the page
                    LinkAnnotation link = new LinkAnnotation(page, linkRect);
                    link.Color = Aspose.Pdf.Color.Blue; // optional visual cue
                    link.Action = new GoToURIAction(originalImageUrl);
                    // Border must be created after the annotation instance exists
                    link.Border = new Border(link) { Width = 1 };

                    // Add the annotation to the page
                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders and hyperlinks: {outputPdfPath}");
    }
}
