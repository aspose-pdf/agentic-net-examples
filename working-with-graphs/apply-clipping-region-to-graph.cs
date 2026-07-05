using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_graph.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (left, bottom, width, height)
            // Shapes drawn outside this area will be clipped
            float clipLeft = 100f;
            float clipBottom = 400f;
            float clipWidth = 300f;
            float clipHeight = 200f;

            // Build a clipping path: a rectangle path followed by the Clip operator (non‑zero winding rule)
            // Use the page.Contents collection (the Operators property was removed in newer versions)
            page.Contents.Add(new MoveTo(clipLeft, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom));
            page.Contents.Add(new LineTo(clipLeft + clipWidth, clipBottom + clipHeight));
            page.Contents.Add(new LineTo(clipLeft, clipBottom + clipHeight));
            page.Contents.Add(new ClosePath());
            page.Contents.Add(new Clip());

            // Create a Graph container (acts like a paragraph) to hold shapes
            // Graph constructor expects double values for width/height
            Graph graph = new Graph(500.0, 800.0);

            // Example shape: a large red rectangle that exceeds the clipping area
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 350f, 500f, 300f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightCoral,
                Color = Color.DarkRed,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Example shape: a blue line crossing the clipping region
            float[] linePoints = { 0f, 0f, 600f, 800f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page; only the parts inside the clipping rectangle will be visible
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
