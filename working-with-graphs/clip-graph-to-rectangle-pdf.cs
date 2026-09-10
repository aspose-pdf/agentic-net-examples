using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (left, bottom, width, height)
            // This rectangle will act as the clipping region.
            float clipLeft = 100f;
            float clipBottom = 400f;
            float clipWidth = 300f;
            float clipHeight = 200f;

            // Build a path that represents the clipping rectangle.
            // The path consists of MoveTo and LineTo operators followed by the Clip operator.
            // Use the page's Contents collection (not a non‑existent Operators property).
            page.Contents.Add(new MoveTo(clipLeft, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom + clipHeight));
            page.Contents.Add(new LineTo(clipLeft, clipBottom + clipHeight));
            page.Contents.Add(new ClosePath()); // Close the rectangle path
            page.Contents.Add(new Clip());      // Apply the clipping path (non‑zero winding rule)

            // Create a Graph container to hold shapes.
            // The Graph will be positioned at (0,0) and its size covers the whole page.
            Graph graph = new Graph(500f, 800f);

            // Add a rectangle shape that extends beyond the clipping region.
            // Only the portion inside the clipping rectangle will be visible.
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 350f, 400f, 300f); // left, bottom, width, height
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Add a circle shape that also exceeds the clipping bounds.
            // The clipping region will trim the visible part.
            Ellipse ellipse = new Ellipse(200f, 300f, 250f, 250f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGreen,
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f
            };
            graph.Shapes.Add(ellipse);

            // Add the graph to the page. Because the clipping path was set earlier,
            // only the parts of the shapes that lie within the clipping rectangle will be rendered.
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("ClippedGraph.pdf");
        }

        Console.WriteLine("PDF with clipped graph saved as 'ClippedGraph.pdf'.");
    }
}
