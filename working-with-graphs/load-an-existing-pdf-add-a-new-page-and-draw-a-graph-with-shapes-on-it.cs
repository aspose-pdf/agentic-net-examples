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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page to the document
            Page newPage = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            // Use double literals as required by the Graph constructor
            Graph graph = new Graph(400.0, 200.0);

            // ----- Rectangle shape (Aspose.Pdf.Drawing.Rectangle) -----
            // Parameters: left, bottom, width, height (float values)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height (float values)
            Ellipse ellipse = new Ellipse(300f, 50f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Line shape -----
            // Points array: { x1, y1, x2, y2 }
            float[] linePoints = { 0f, 0f, 350f, 150f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the Graph (with its shapes) to the new page
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
