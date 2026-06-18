using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end
            Page newPage = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt)
            Graph graph = new Graph(400, 200);

            // ----- Rectangle shape -----
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Constructor: left, bottom, width, height
            Ellipse ellipseShape = new Ellipse(300, 50, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // ----- Line shape -----
            // Constructor takes an array: { x1, y1, x2, y2 }
            float[] linePoints = { 50, 200, 350, 200 };
            Line lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 3
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page's paragraphs collection
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and graphics: {outputPath}");
    }
}