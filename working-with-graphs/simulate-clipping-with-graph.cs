using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_shapes.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (left, bottom, width, height)
            float clipLeft   = 100f;
            float clipBottom = 400f;
            float clipWidth  = 300f;
            float clipHeight = 200f;

            // Create a Graph container (width, height) – size large enough to hold all shapes
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 800.0);

            // -----------------------------------------------------------------
            // 1. Define the clipping path (visual only – actual clipping via operators is not supported in Graph)
            // -----------------------------------------------------------------
            // The rectangle that represents the clipping region. It is added as a shape
            // but made invisible so it does not appear in the final PDF.
            var clipRect = new Aspose.Pdf.Drawing.Rectangle(
                clipLeft,
                clipBottom,
                clipWidth,
                clipHeight);
            clipRect.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Transparent,
                FillColor = Aspose.Pdf.Color.Transparent,
                LineWidth = 0f
            };
            graph.Shapes.Add(clipRect);

            // -----------------------------------------------------------------
            // 2. Add shapes that would be clipped if a clipping operator were applied
            // -----------------------------------------------------------------
            // A large red rectangle that extends beyond the clipping bounds
            var redRect = new Aspose.Pdf.Drawing.Rectangle(50f, 350f, 400f, 300f);
            redRect.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Red,
                FillColor = Aspose.Pdf.Color.Red,
                LineWidth = 1f
            };
            graph.Shapes.Add(redRect);

            // A blue ellipse that partially lies outside the clipping region
            var blueEllipse = new Ellipse(150f, 450f, 350f, 650f);
            blueEllipse.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                FillColor = Aspose.Pdf.Color.Blue,
                LineWidth = 1f
            };
            graph.Shapes.Add(blueEllipse);

            // -----------------------------------------------------------------
            // 3. Add the graph to the page
            // -----------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shapes saved to '{outputPath}'.");
    }
}
