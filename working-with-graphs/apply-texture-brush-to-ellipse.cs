using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class TextureEllipseExample
{
    static void Main()
    {
        // Input image to be used as texture
        const string texturePath = "texture.png";

        // Output PDF file
        const string outputPdf = "ellipse_with_texture.pdf";

        // Verify that the texture image exists
        if (!File.Exists(texturePath))
        {
            Console.Error.WriteLine($"Texture image not found: {texturePath}");
            return;
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define ellipse bounds (left, bottom, width, height)
            double left   = 100;
            double bottom = 400;
            double width  = 300;
            double height = 200;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            ellipse.GraphInfo = new GraphInfo
            {
                // Optional: set outline color and width
                Color     = Color.Black,
                LineWidth = 1f
            };

            // Graph is a container for drawing objects. It requires width and height in its constructor.
            // The size can be larger than the ellipse; the ellipse uses absolute page coordinates.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Create an ImageStamp positioned exactly over the ellipse bounds.
            // This stamp paints the image; because it is placed over the ellipse outline, it visually acts as a texture fill.
            ImageStamp textureStamp = new ImageStamp(texturePath)
            {
                Background = false, // draw over the ellipse outline
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = (float)left,
                YIndent = (float)bottom,
                Width  = (float)width,
                Height = (float)height
            };

            // Add the stamp to the page – this paints the image inside the ellipse area.
            page.AddStamp(textureStamp);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Ellipse with texture saved to '{outputPdf}'.");
    }
}
