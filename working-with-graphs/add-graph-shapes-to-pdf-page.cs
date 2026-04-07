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

        // Load the existing PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page (pages are 1‑based)
            Page newPage = doc.Pages.Add();

            // Graph constructor now expects double values
            Graph graph = new Graph(400.0, 200.0);

            // ---- Rectangle shape (Aspose.Pdf.Drawing.Rectangle) ----
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 100f, 50f); // left, bottom, width, height
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ---- Ellipse shape ----
            var ellipse = new Ellipse(200f, 50f, 100f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---- Line shape ----
            float[] linePoints = { 50f, 180f, 350f, 180f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph (which contains the shapes) to the page
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
