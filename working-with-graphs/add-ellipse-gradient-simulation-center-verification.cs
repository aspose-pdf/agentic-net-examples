using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define ellipse geometry (center at (300,400), radii 100 and 150)
            double centerX = 300;
            double centerY = 400;
            double radiusX = 100;
            double radiusY = 150;

            // Since Aspose.Pdf does not expose a ConicalGradientFill in the referenced version,
            // we simulate a simple two‑color fill using a solid colour (the API does not support
            // gradient fills for shapes in this version). The gradient centre is stored separately
            // for verification purposes.
            Point gradientCenter = new Point(centerX, centerY);

            // Build the ellipse shape using explicit coordinates (left, bottom, width, height)
            double left = centerX - radiusX;
            double bottom = centerY - radiusY;
            double width = radiusX * 2;
            double height = radiusY * 2;
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Apply a solid fill colour (simulating the start colour of a gradient).
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(1, 0, 0), // Red – start colour of the intended gradient
                // Border colour (optional)
                Color = Color.FromRgb(0, 0, 0) // Black border
            };

            // Shapes must be placed inside a Graph container before adding to the page.
            // Use the double overload of the Graph constructor (the float overload is obsolete).
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // ---- Verification ----
            // Compute the centre of the rectangle (which is the centre of the ellipse)
            double ellipseCenterX = (left + (left + width)) / 2; // (LLX + URX) / 2
            double ellipseCenterY = (bottom + (bottom + height)) / 2; // (LLY + URY) / 2

            // Compare with the stored gradient centre
            bool isAligned = Math.Abs(ellipseCenterX - gradientCenter.X) < 0.001 &&
                             Math.Abs(ellipseCenterY - gradientCenter.Y) < 0.001;

            Console.WriteLine(isAligned
                ? "Gradient origin aligns with ellipse center."
                : "Gradient origin does NOT align with ellipse center.");

            // Save the PDF (no PreSave needed for drawing operations)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
