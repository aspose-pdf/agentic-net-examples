using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a Graph container with desired dimensions
            double graphWidth = 400;
            double graphHeight = 200;
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(graphWidth, graphHeight);

            // Apply a 150% scaling to all shapes within the graph
            Aspose.Pdf.GraphInfo graphInfo = new Aspose.Pdf.GraphInfo
            {
                ScalingRateX = 1.5,
                ScalingRateY = 1.5
            };
            graph.GraphInfo = graphInfo;

            // Add a rectangle shape
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Add an ellipse shape
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(150, 0, 250, 100);
            ellipse.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipse);

            // Add a line shape
            float[] linePoints = { 0, 150, 300, 150 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Insert the graph into the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}