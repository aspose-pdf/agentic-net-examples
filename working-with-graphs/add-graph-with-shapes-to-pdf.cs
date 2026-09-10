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

        // Load the existing PDF (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page (lifecycle: create)
            Page newPage = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            // Use double parameters as the constructor with float is obsolete.
            Graph graph = new Graph(400.0, 200.0);

            // ----- Draw a rectangle -----
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Draw an ellipse -----
            // Ellipse constructor: (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(150f, 0f, 100f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Draw a line -----
            // Line constructor takes a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 0f, 100f, 300f, 150f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the Graph (which contains the shapes) to the page's paragraphs
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
