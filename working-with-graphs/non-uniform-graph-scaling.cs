using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Use the double‑parameter constructor for Graph (the float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0);

            // Configure non‑uniform scaling via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5,
                ScalingRateY = 0.5,
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };

            // Add a rectangle shape to demonstrate the scaling effect.
            // NOTE: Use Aspose.Pdf.Drawing.Rectangle (the drawing shape), not Aspose.Pdf.Rectangle (page rectangle).
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
