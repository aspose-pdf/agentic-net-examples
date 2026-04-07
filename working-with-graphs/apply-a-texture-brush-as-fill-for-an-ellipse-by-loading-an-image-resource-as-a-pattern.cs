using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class TextureEllipseExample
{
    static void Main()
    {
        // Input image that will be used as a texture pattern
        const string texturePath = "texture.png";

        // Output PDF file
        const string outputPdf = "ellipse_with_texture.pdf";

        // Verify that the texture image exists
        if (!File.Exists(texturePath))
        {
            Console.Error.WriteLine($"Texture image not found: {texturePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // STEP 1 – Load the image and add it to the page resources.
            // The image is stored once in the PDF and can be referenced by name.
            // ------------------------------------------------------------
            string imageName;
            using (FileStream imgStream = File.OpenRead(texturePath))
            {
                // Add the image stream to the page's image collection.
                // The method returns the internal name assigned to the image.
                imageName = page.Resources.Images.Add(imgStream);
            }

            // ------------------------------------------------------------
            // STEP 2 – Define the ellipse bounds.
            // The ellipse will be drawn at (100,400) with a width of 300 and height of 200.
            // ------------------------------------------------------------
            double left   = 100;   // X‑coordinate of the lower‑left corner
            double bottom = 400;   // Y‑coordinate of the lower‑left corner
            double width  = 300;
            double height = 200;

            // ------------------------------------------------------------
            // STEP 3 – Add the texture image to the page, positioned exactly
            // inside the ellipse's bounding rectangle.
            // ------------------------------------------------------------
            // The rectangle used for the image must match the ellipse bounds.
            Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(left, bottom, left + width, bottom + height);
            // Place the image on the page. The image will be drawn first.
            page.AddImage(imageName, imgRect);

            // ------------------------------------------------------------
            // STEP 4 – Draw the ellipse on top of the image.
            // The ellipse is added as a vector shape with a transparent fill
            // (no FillColor) and a visible border.
            // ------------------------------------------------------------
            // Create a Graph container (vector graphics) and add the ellipse shape.
            Graph graph = new Graph(width, height);
            // The ellipse constructor expects (left, bottom, width, height) relative to the graph.
            Ellipse ellipse = new Ellipse(0, 0, width, height);
            // Configure visual appearance via GraphInfo.
            ellipse.GraphInfo = new GraphInfo
            {
                // No fill color – the underlying image shows through.
                FillColor = Aspose.Pdf.Color.Transparent,
                // Border color and line width.
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2
            };
            // Add the ellipse to the graph.
            graph.Shapes.Add(ellipse);
            // Position the graph at the same location as the image.
            graph.Margin = new MarginInfo(left, 0, 0, bottom);
            // Add the graph to the page's paragraphs collection.
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // STEP 5 – Save the document.
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with textured ellipse saved to '{outputPdf}'.");
    }
}