using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_shapes.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Initialize a Graph that covers the whole page
            double graphWidth = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;
            Graph graph = new Graph(graphWidth, graphHeight);

            // Apply a 150 % scaling transformation to all shapes in the graph
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5, // 150 % on X axis
                ScalingRateY = 1.5  // 150 % on Y axis
            };

            // Example shape: a rectangle (position and size are in points)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Add the graph (with its scaled shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}