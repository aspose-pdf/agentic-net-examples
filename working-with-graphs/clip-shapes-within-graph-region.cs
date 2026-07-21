using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_shapes.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph container (width, height) – use double literals as required by the API
            Graph graph = new Graph(500.0, 400.0);

            // Optional visual rectangle that represents the intended clipping region.
            // Use Aspose.Pdf.Drawing.Rectangle for drawing shapes (not Aspose.Pdf.Rectangle).
            var clipRect = new Aspose.Pdf.Drawing.Rectangle(100f, 100f, 300f, 200f);
            clipRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Transparent,
                FillColor = Aspose.Pdf.Color.Transparent
            };
            // Adding the rectangle as a transparent shape – it does not affect drawing but shows the region.
            graph.Shapes.Add(clipRect);

            // Large rectangle that extends beyond the intended region.
            var largeRect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 400f, 300f);
            largeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(largeRect);

            // Ellipse that also exceeds the bounds.
            var ellipse = new Ellipse(150f, 150f, 350f, 300f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Orange,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Add the graph (with its shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
