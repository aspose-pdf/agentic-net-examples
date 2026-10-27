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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph object (size 500x500, positioned at (0,0))
            Graph graph = new Graph(500, 500);
            graph.Left = 0;
            graph.Top = 0;

            // Add a rectangle shape and scale it by 150 %
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 150);
            rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;   // fill
            rectShape.GraphInfo.Color = Aspose.Pdf.Color.Black;          // stroke
            rectShape.GraphInfo.ScalingRateX = 1.5f;                     // 150 % X scale
            rectShape.GraphInfo.ScalingRateY = 1.5f;                     // 150 % Y scale
            graph.Shapes.Add(rectShape);

            // Add an ellipse shape and scale it by 150 %
            var ellipseShape = new Ellipse(250, 250, 400, 400);
            ellipseShape.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue;
            ellipseShape.GraphInfo.Color = Aspose.Pdf.Color.DarkBlue;
            ellipseShape.GraphInfo.ScalingRateX = 1.5f;
            ellipseShape.GraphInfo.ScalingRateY = 1.5f;
            graph.Shapes.Add(ellipseShape);

            // Render the graph on the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
