using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_shapes.pdf";

        // Document lifecycle: create, use, and save inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph with a defined width and height
            double graphWidth = 400;
            double graphHeight = 200;
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(graphWidth, graphHeight);

            // Apply a 150 % scaling transformation to all shapes within the graph
            // ScalingRateX/Y are part of GraphInfo and affect the coordinate system of the graph
            graph.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                ScalingRateX = 1.5, // 150 % scaling on X axis
                ScalingRateY = 1.5  // 150 % scaling on Y axis
            };

            // ----- Add a rectangle shape -----
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 100, 60);
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // ----- Add an ellipse shape -----
            Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(200, 50, 80, 60);
            ellipseShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipseShape);

            // ----- Add a line shape -----
            float[] linePoints = { 50, 150, 300, 150 };
            Aspose.Pdf.Drawing.Line lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
            lineShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(lineShape);

            // Add the fully configured graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }
    }
}