using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "skewed_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (use using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a graph – the constructor now expects double values
            Graph graph = new Graph(200.0, 100.0);

            // Apply a skew on the X axis (float value)
            graph.GraphInfo.SkewAngleX = 30f;

            // Optional visual styling for the graph background
            graph.GraphInfo.FillColor = Color.LightGray;
            graph.GraphInfo.Color = Color.Black;
            graph.GraphInfo.LineWidth = 1f;

            // Add a rectangle shape inside the graph to illustrate the skew effect
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.DarkBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Skewed graph PDF saved to '{outputPath}'.");
    }
}
