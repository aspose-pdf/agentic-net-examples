using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_placeholders.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Access the collection of images on the current page
                XImageCollection images = page.Resources.Images;

                // XImageCollection is 1‑based; iterate safely
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Create a placeholder rectangle (size 100×100 points) positioned at (100,500)
                    // Adjust coordinates as needed for your layout
                    float llx = 100f;
                    float lly = 500f;
                    float urx = llx + 100f;
                    float ury = lly + 100f;

                    // Draw the rectangle using a Graph container
                    Graph placeholderGraph = new Graph(400f, 200f);
                    Aspose.Pdf.Drawing.Rectangle placeholderRect = new Aspose.Pdf.Drawing.Rectangle(llx, lly, urx - llx, ury - lly);
                    placeholderRect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };
                    placeholderGraph.Shapes.Add(placeholderRect);
                    page.Paragraphs.Add(placeholderGraph);

                    // Create a hyperlink annotation that points to the original image location.
                    // Here we use a placeholder URL; replace with the real image URL if available.
                    string imageUrl = $"https://example.com/original_image_{pageNum}_{imgIndex}.png";

                    // The annotation rectangle must match the placeholder rectangle coordinates.
                    Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    LinkAnnotation link = new LinkAnnotation(page, linkRect)
                    {
                        // Use GoToURIAction for external URLs
                        Action = new GoToURIAction(imageUrl)
                    };
                    page.Annotations.Add(link);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved as '{outputPdf}'.");
    }
}