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

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page to the document
            Page newPage = doc.Pages.Add();

            // Graph constructor now expects double values (width, height)
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle shape (drawing) -----
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) and pass float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 200f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            var ellipse = new Ellipse(200f, 200f, 100f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Line shape -----
            float[] linePoints = { 50f, 50f, 350f, 250f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph (containing the shapes) to the new page
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}