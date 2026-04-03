using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string imagePath   = "texture.jpg";   // image to use as texture
        const string outputPath  = "ellipse_with_texture.pdf";

        // Verify the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document (lifecycle rule: use using block)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define ellipse bounds (left, bottom, width, height)
            // These values are in points (1/72 inch)
            double left   = 100;
            double bottom = 400;
            double width  = 300;
            double height = 200;

            // -----------------------------------------------------------------
            // STEP 1 – Add the texture image covering the same area as the ellipse
            // -----------------------------------------------------------------
            // The image is added directly to the page using the AddImage method.
            // This effectively paints the image as the "fill" for the region.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Rectangle expects (llx, lly, urx, ury)
                var imgRect = new Aspose.Pdf.Rectangle(
                    left,
                    bottom,
                    left + width,
                    bottom + height);

                page.AddImage(imgStream, imgRect);
            }

            // ---------------------------------------------------------------
            // STEP 2 – Draw the ellipse border on top of the image (optional)
            // ---------------------------------------------------------------
            // Create a Graph container that holds vector shapes.
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create the ellipse shape with the same bounds.
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(left, bottom, (float)width, (float)height);

            // Set visual properties via GraphInfo.
            ellipse.GraphInfo = new GraphInfo
            {
                // No fill – we already have the image as background.
                FillColor = Aspose.Pdf.Color.Transparent,
                // Border color and thickness
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };

            // Add the ellipse to the graph.
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's content.
            page.Paragraphs.Add(graph);

            // ---------------------------------------------------------------
            // STEP 3 – Save the document (lifecycle rule: use doc.Save)
            // ---------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}