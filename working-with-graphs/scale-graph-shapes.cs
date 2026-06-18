using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace GraphTransformExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the PDF and add a graph whose shapes are scaled by 150%
            using (Document doc = new Document("input.pdf"))
            {
                // Create a graph of size 200 x 100 points
                Graph graph = new Graph(200.0, 100.0);
                // Apply scaling (150%) to the whole graph via GraphInfo
                graph.GraphInfo = new GraphInfo();
                graph.GraphInfo.ScalingRateX = 1.5;
                graph.GraphInfo.ScalingRateY = 1.5;

                // Rectangle shape
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(10, 10, 80, 40);
                rectShape.GraphInfo = new GraphInfo();
                rectShape.GraphInfo.FillColor = Color.Yellow;
                rectShape.GraphInfo.Color = Color.Black;
                rectShape.GraphInfo.LineWidth = 1.0f;

                // Ellipse shape
                Ellipse ellipseShape = new Ellipse(110, 10, 80, 40);
                ellipseShape.GraphInfo = new GraphInfo();
                ellipseShape.GraphInfo.FillColor = Color.LightBlue;
                ellipseShape.GraphInfo.Color = Color.DarkBlue;
                ellipseShape.GraphInfo.LineWidth = 1.0f;

                // Add shapes to the graph (collection limit respected)
                graph.Shapes.Add(rectShape);
                graph.Shapes.Add(ellipseShape);

                // Add the graph to the first page (1‑based indexing)
                Page page = doc.Pages[1];
                page.Paragraphs.Add(graph);

                // Save the result
                doc.Save("output.pdf");
            }
        }
    }
}
