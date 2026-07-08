using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a canvas) with desired dimensions
            Graph graph = new Graph(500, 500); // width = 500, height = 500

            // Create an unfilled arc:
            //   Center at (250, 250), radius = 100,
            //   start angle (alpha) = 0°, end angle (beta) = 180°
            Arc arc = new Arc(250, 250, 100, 0, 180);

            // Configure visual appearance via GraphInfo:
            //   - Set line width
            //   - Define dash pattern (5 units on, 3 units off)
            //   - No fill color is set, so the arc remains unfilled
            arc.GraphInfo = new GraphInfo
            {
                LineWidth = 2f,
                DashArray = new int[] { 5, 3 }
            };

            // Add the arc to the Graph's shape collection
            graph.Shapes.Add(arc);

            // Place the Graph on the page
            page.Paragraphs.Add(graph);

            // Save the PDF to disk
            doc.Save("arc_output.pdf");
        }
    }
}