using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph with specified width and height
            Graph graph = new Graph(400, 200);

            // Add a rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            graph.Shapes.Add(rectShape);

            // Add an ellipse shape to the graph
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(150, 0, 250, 100);
            graph.Shapes.Add(ellipse);

            // Add a line shape to the graph
            float[] linePoints = { 0, 150, 300, 150 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            graph.Shapes.Add(line);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Iterate through all shapes in the graph and log their bounding boxes
            foreach (Shape shape in graph.Shapes)
            {
                if (shape is Aspose.Pdf.Drawing.Rectangle r)
                {
                    double left   = r.Left;
                    double bottom = r.Bottom;
                    double right  = left + r.Width;
                    double top    = bottom + r.Height;
                    Console.WriteLine($"Rectangle - L:{left}, B:{bottom}, R:{right}, T:{top}");
                }
                else if (shape is Aspose.Pdf.Drawing.Ellipse e)
                {
                    double left   = e.Left;
                    double bottom = e.Bottom;
                    double right  = left + e.Width;
                    double top    = bottom + e.Height;
                    Console.WriteLine($"Ellipse - L:{left}, B:{bottom}, R:{right}, T:{top}");
                }
                else if (shape is Aspose.Pdf.Drawing.Line)
                {
                    // Bounding box for a line is not directly exposed; log the shape type.
                    Console.WriteLine("Line - bounding box not directly available.");
                }
                else
                {
                    Console.WriteLine($"Shape type {shape.GetType().Name} - bounding box handling not implemented.");
                }
            }

            // Save the document (optional)
            doc.Save("output.pdf");
        }
    }
}