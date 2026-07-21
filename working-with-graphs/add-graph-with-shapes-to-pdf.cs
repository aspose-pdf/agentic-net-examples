using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page
            Page newPage = doc.Pages.Add();

            // Graph constructor expects double values (width, height)
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle (drawing shape) -----
            // Use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse -----
            var ellipse = new Ellipse(300f, 150f, 80f, 120f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Line -----
            // Coordinates are {x1, y1, x2, y2}
            float[] linePoints = { 100f, 200f, 350f, 200f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the Graph to the page's paragraph collection
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
