using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_graph.pdf";
        const double rotationDegrees = 45; // rotation angle in degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page to place the graph
            Page page = doc.Pages.Add();

            // Define graph size (width x height)
            double graphWidth = 300.0;
            double graphHeight = 200.0;

            // Create the graph with explicit width and height
            Graph graph = new Graph(graphWidth, graphHeight);

            // Position the graph on the page (coordinates are from the lower‑left corner)
            graph.Left = 100;   // X position
            graph.Top = 500;    // Y position

            // Apply rotation and fill color via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                RotationAngle = rotationDegrees,
                FillColor = Color.LightGray
            };

            // Set a border for the graph
            graph.Border = new BorderInfo(BorderSide.All, 1, Color.Black);

            // Add a simple line shape as sample data
            Line line = new Line(new float[] { 0, 0, 300, 0 });
            line.GraphInfo.Color = Color.Blue;
            line.GraphInfo.LineWidth = 2;
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated graph PDF to '{outputPath}'.");
    }
}
