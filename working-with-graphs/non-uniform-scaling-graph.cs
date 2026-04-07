using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Use the double‑based Graph constructor (width, height)
            Graph graph = new Graph(400.0, 200.0);

            // Example shape: a rectangle inside the graph
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) – it expects float values
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,          // lower‑left X
                0f,          // lower‑left Y
                100f,        // width
                50f);        // height

            // Set shape appearance via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Apply non‑uniform scaling via GraphInfo on the graph itself
            // ScalingRateX > 1 stretches horizontally, ScalingRateY < 1 compresses vertically
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5f, // 150 % width
                ScalingRateY = 0.5f  // 50 % height
            };

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
