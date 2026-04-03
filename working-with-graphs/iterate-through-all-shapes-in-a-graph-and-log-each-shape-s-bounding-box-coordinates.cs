using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a Graph container (width, height in points)
            Graph graph = new Graph(500, 400);

            // Add a rectangle shape
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 300, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // Add an ellipse shape
            Ellipse ellipseShape = new Ellipse(250, 300, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(graph);

            // Iterate through all shapes in the graph and log their bounding boxes
            foreach (Shape shape in graph.Shapes)
            {
                if (shape is Aspose.Pdf.Drawing.Rectangle r)
                {
                    double llx = r.Left;
                    double lly = r.Bottom;
                    double urx = r.Left + r.Width;
                    double ury = r.Bottom + r.Height;
                    Console.WriteLine($"Rectangle - LLX:{llx}, LLY:{lly}, URX:{urx}, URY:{ury}");
                }
                else if (shape is Ellipse e)
                {
                    double llx = e.Left;
                    double lly = e.Bottom;
                    double urx = e.Left + e.Width;
                    double ury = e.Bottom + e.Height;
                    Console.WriteLine($"Ellipse - LLX:{llx}, LLY:{lly}, URX:{urx}, URY:{ury}");
                }
                else
                {
                    // Other shape types (e.g., Path) do not expose explicit bounds via properties
                    Console.WriteLine($"Shape of type {shape.GetType().Name} does not have direct bounding box properties.");
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}