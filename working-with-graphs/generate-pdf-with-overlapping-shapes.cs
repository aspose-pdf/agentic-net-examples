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

            // Create a Graph (acts as a container for vector shapes)
            // Width = 400 points, Height = 300 points
            Graph graph = new Graph(400, 300)
            {
                // Position the graph on the page (coordinates are from bottom‑left)
                Left = 50,
                Top  = 500
            };

            // ----- Shape 1: Rectangle -----
            // Parameters: left, bottom, width, height
            var rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                }
            };
            graph.Shapes.Add(rect);

            // ----- Shape 2: Circle (actually an ellipse with equal width/height) -----
            // Parameters: left, bottom, width, height
            var circle = new Aspose.Pdf.Drawing.Ellipse(150, 150, 80, 80)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color     = Aspose.Pdf.Color.Red,
                    LineWidth = 2
                }
            };
            graph.Shapes.Add(circle);

            // ----- Overlap handling -----
            // The older BoundsCheckMode API was removed from recent Aspose.Pdf versions.
            // Shapes are placed within the graph's dimensions, so explicit overlap prevention
            // is not required. If needed, manual coordinate adjustments can be applied here.

            // Add the graph (with its shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("OverlappingShapes.pdf");
        }

        Console.WriteLine("PDF created: OverlappingShapes.pdf");
    }
}