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
            // Graph constructor expects width and height as double values
            Graph graph = new Graph(400.0, 200.0);

            // ----- Add a rectangle shape -----
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0.0F, 0.0F, 100.0F, 50.0F);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2.0F
            };
            graph.Shapes.Add(rectShape);

            // ----- Add an ellipse shape -----
            // Ellipse(left, bottom, width, height)
            Ellipse ellipseShape = new Ellipse(150.0F, 100.0F, 200.0F, 150.0F);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5F
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            // ----- Iterate through all shapes in the graph and log their bounding boxes -----
            Console.WriteLine("Shape bounding boxes in the graph:");
            foreach (Shape shape in graph.Shapes)
            {
                // Determine shape type and extract bounding information accordingly
                if (shape is Aspose.Pdf.Drawing.Rectangle r)
                {
                    Console.WriteLine($"Rectangle - Left: {r.Left}, Bottom: {r.Bottom}, Width: {r.Width}, Height: {r.Height}");
                }
                else if (shape is Ellipse e)
                {
                    Console.WriteLine($"Ellipse - Left: {e.Left}, Bottom: {e.Bottom}, Width: {e.Width}, Height: {e.Height}");
                }
                else if (shape is Aspose.Pdf.Drawing.Path p)
                {
                    // Path does not expose direct coordinates; log its type only
                    Console.WriteLine("Path shape - bounding box not directly accessible");
                }
                else
                {
                    Console.WriteLine($"Shape of type {shape.GetType().Name} - bounding box not directly accessible");
                }
            }

            // Save the PDF (optional, can be omitted if only logging is required)
            doc.Save("GraphWithShapes.pdf");
        }
    }
}
