using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "gradient_background.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a BackgroundArtifact and place it behind page contents
            // ------------------------------------------------------------
            BackgroundArtifact bgArtifact = new BackgroundArtifact
            {
                // Ensure the artifact is rendered as a background
                IsBackground = true
            };
            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(bgArtifact);

            // ------------------------------------------------------------
            // Simulate a vertical gradient by drawing many thin rectangles
            // with interpolated colors. The drawing is added to the page
            // after the artifact, so it appears behind the page content.
            // ------------------------------------------------------------
            // Create a Graph that covers the whole page
            Graph gradientGraph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define start and end colors for the gradient (e.g., blue to white)
            // Aspose.Pdf.Color uses normalized values (0‑1) for FromRgb.
            double startR = 0.0, startG = 0.0, startB = 1.0; // Blue
            double endR   = 1.0, endG   = 1.0, endB   = 1.0; // White

            int steps = 200; // Number of gradient steps (more steps = smoother gradient)
            double stripeHeight = page.PageInfo.Height / steps;

            for (int i = 0; i < steps; i++)
            {
                // Linear interpolation factor (0..1)
                double t = (double)i / (steps - 1);

                // Interpolate each RGB component
                double r = startR + t * (endR - startR);
                double g = startG + t * (endG - startG);
                double b = startB + t * (endB - startB);

                // Create the interpolated color
                Aspose.Pdf.Color fillColor = Aspose.Pdf.Color.FromRgb(r, g, b);

                // Compute Y position for the current stripe (origin is bottom‑left)
                double y = page.PageInfo.Height - (i + 1) * stripeHeight;

                // Create a rectangle stripe covering the full width
                var stripe = new Aspose.Pdf.Drawing.Rectangle(
                    (float)0,
                    (float)y,
                    (float)page.PageInfo.Width,
                    (float)stripeHeight);
                stripe.GraphInfo = new GraphInfo
                {
                    FillColor = fillColor,
                    Color = fillColor,   // Border color (same as fill, line width 0)
                    LineWidth = 0f       // float literal as required
                };

                // Add the stripe to the graph
                gradientGraph.Shapes.Add(stripe);
            }

            // Add the gradient graph to the page. Because the BackgroundArtifact
            // is marked as IsBackground, the graph will be rendered behind any
            // other page content.
            page.Paragraphs.Add(gradientGraph);

            // Save the PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient background saved to '{outputPath}'.");
    }
}
