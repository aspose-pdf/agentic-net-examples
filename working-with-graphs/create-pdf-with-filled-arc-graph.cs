using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container with desired size (width, height in points)
            Graph graph = new Graph(400, 300);

            // Create an Arc:
            //   center at (200,150), radius 100, start angle 0°, end angle 180°
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Set visual properties via GraphInfo (filled with a custom color)
            arc.GraphInfo = new GraphInfo
            {
                // Light blue fill using RGB components (values 0.0‑1.0)
                FillColor = Color.FromRgb(0.4, 0.7, 1.0)
            };

            // Add the Arc to the Graph's shape collection
            graph.Shapes.Add(arc);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("ArcGraph.pdf");
        }
    }
}