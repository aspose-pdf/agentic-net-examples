using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_ellipse.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container that will hold vector shapes
            // Width and height define the drawing canvas size; choose values larger than the shape
            Graph graph = new Graph(400, 200);

            // Define an ellipse shape (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 100, 200, 150);
            // Set visual properties via GraphInfo (fill color, border color, line width, etc.)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph (with the ellipse) to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse added and saved to '{outputPath}'.");
    }
}