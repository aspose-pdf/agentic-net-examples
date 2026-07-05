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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a Graph container (acts like a drawing canvas)
            Graph graph = new Graph(400, 200); // width = 400 pt, height = 200 pt

            // Add a rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // Define the original placement rectangle for the graph
            Aspose.Pdf.Rectangle placementRect = new Aspose.Pdf.Rectangle(100, 500, 500, 700);
            // Translate (move) the rectangle by the desired offsets
            double dx = 50;   // move right by 50 points
            double dy = -30;  // move down by 30 points
            placementRect.MoveBy(dx, dy);

            // Position the graph using the translated rectangle.
            // Graph.Left sets the X coordinate (lower‑left X).
            // Graph.Top sets the Y coordinate of the upper side.
            graph.Left = placementRect.LLX;
            graph.Top = placementRect.URY;

            // Add the graph to the first page of the document
            doc.Pages[1].Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph translated and saved to '{outputPath}'.");
    }
}