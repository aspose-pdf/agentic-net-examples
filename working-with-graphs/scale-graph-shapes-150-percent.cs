using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a graph with desired size (width, height)
            Graph graph = new Graph(400, 200);

            // Scale all shapes by 150 % using GraphInfo scaling rates
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5,
                ScalingRateY = 1.5
            };

            // ----- Rectangle shape -----
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            Ellipse ellipse = new Ellipse(150, 0, 250, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Line shape -----
            float[] linePoints = { 0, 150, 300, 150 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with scaled graph saved to '{outputPath}'.");
    }
}