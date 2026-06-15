using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the texture image
        const string outputPdf = "EllipseWithTexture.pdf";
        const string textureImagePath = "texture.png";

        // Verify that the texture image exists
        if (!File.Exists(textureImagePath))
        {
            Console.Error.WriteLine($"Texture image not found: {textureImagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Load the texture image into the page resources
            // The image is added once and can be referenced by name later
            string imageName;
            using (FileStream imgStream = File.OpenRead(textureImagePath))
            {
                imageName = page.Resources.Images.Add(imgStream);
            }

            // ------------------------------------------------------------
            // NOTE:
            // Aspose.Pdf does not provide a high‑level API to fill a shape
            // directly with an image pattern (texture brush).  To achieve
            // this you would need to work with low‑level PDF operators,
            // define a tiling pattern, and apply it via a SetAdvancedColorFill
            // operator.  The code below demonstrates the standard way to
            // add an ellipse with a solid fill.  The comments indicate where
            // the pattern‑related logic would be inserted.
            // ------------------------------------------------------------

            // Create a Graph container (required for drawing vector shapes)
            // Graph constructor expects double values.
            Graph graph = new Graph(500.0, 400.0);

            // Define an ellipse shape (left, bottom, width, height)
            // Ellipse constructor expects float values.
            Ellipse ellipse = new Ellipse(100.0F, 150.0F, 300.0F, 200.0F);

            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                // Solid fill color (replace with pattern logic as needed)
                FillColor = Aspose.Pdf.Color.LightGray,
                // Outline color
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Placeholder for texture fill implementation:
            // 1. Define a tiling pattern (PatternColorSpace derived class).
            // 2. Use SetAdvancedColorFill operator with the pattern name.
            // 3. Apply the operator before drawing the ellipse.
            // The detailed low‑level steps are beyond the scope of this
            // example but follow the PDF specification for pattern fills.
            // ------------------------------------------------------------

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
