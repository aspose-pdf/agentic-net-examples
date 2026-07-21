using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (left, bottom, width, height)
            // This rectangle will act as the clipping region for subsequent graphics
            float clipLeft   = 100f;
            float clipBottom = 400f;
            float clipWidth  = 300f;
            float clipHeight = 200f;

            // Build the clipping path using low‑level PDF operators.
            // The clipping operators are added directly to the page's content collection.
            page.Contents.Add(new MoveTo(clipLeft, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom + clipHeight));
            page.Contents.Add(new LineTo(clipLeft, clipBottom + clipHeight));
            page.Contents.Add(new ClosePath());
            page.Contents.Add(new Clip()); // apply clipping (non‑zero winding rule)
            // The clipping path stays active for subsequent content streams.

            // Now any subsequent graphics will be rendered only inside the clipping rectangle.
            // Create a Graph container to hold shapes. Use the double‑based constructor as required.
            Graph graph = new Graph(500.0, 500.0);

            // Example shape 1: a large red rectangle that exceeds the clipping bounds
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 400f, 300f);
            rect.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                FillColor = Color.LightCoral,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Example shape 2: a blue ellipse that also exceeds the clipping bounds
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200f, 200f, 350f, 450f);
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                FillColor = Color.LightBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(ellipse);

            // Add the graph to the page. Because the clipping path is already active,
            // only the portions of the shapes that intersect the clipping rectangle will appear.
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("ClippedGraphics.pdf");
        }

        Console.WriteLine("PDF with clipped graphics saved as 'ClippedGraphics.pdf'.");
    }
}
