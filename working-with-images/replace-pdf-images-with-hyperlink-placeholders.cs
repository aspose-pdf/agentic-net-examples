using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_placeholder.pdf";
        const string imageBaseUrl = "https://example.com/images/"; // base URL for original images

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Find all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Hide the original image
                    placement.Hide();

                    // Build a URL that points to the original image.
                    // Use the image name if available; otherwise fall back to a generic placeholder.
                    string imageName = placement.Image.Name ?? $"image_page{pageNum}_idx{placement.Image.GetHashCode()}";
                    string imageUrl = imageBaseUrl + Uri.EscapeDataString(imageName);

                    // Create a hyperlink annotation that occupies the same rectangle as the original image
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        placement.Rectangle.LLX,
                        placement.Rectangle.LLY,
                        placement.Rectangle.URX,
                        placement.Rectangle.URY);

                    LinkAnnotation link = new LinkAnnotation(page, rect)
                    {
                        // Optional visual styling (border can be omitted)
                        Color = Aspose.Pdf.Color.Transparent
                    };
                    link.Action = new GoToURIAction(imageUrl);

                    // Add the annotation to the page
                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image placeholders saved to '{outputPath}'.");
    }
}