using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "skewed_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) that will hold vector shapes
            // Use the double‑precision constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Configure GraphInfo to apply a skew on the X‑axis (e.g., 30 degrees)
            graph.GraphInfo = new GraphInfo
            {
                SkewAngleX = 30
            };

            // Create a rectangle shape to demonstrate the skew
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            // Set visual styling via the shape's GraphInfo (FillColor, Border Color, LineWidth)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Position the graph on the page (optional). Graph does not expose a Bottom property;
            // positioning can be done via Left and Top (or simply rely on the default placement).
            graph.Left = 100;
            // graph.Top = 400; // Uncomment if you need to set the vertical position explicitly.

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
