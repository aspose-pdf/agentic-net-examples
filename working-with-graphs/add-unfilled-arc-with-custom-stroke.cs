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
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container to hold vector shapes
            // Width = 400, Height = 200 (adjust as needed)
            Graph graph = new Graph(400, 200);

            // Create an unfilled Arc:
            // posX = 200 (center X), posY = 100 (center Y)
            // radius = 80, alpha = 0°, beta = 180° (half‑circle)
            Arc arc = new Arc(200f, 100f, 80f, 0f, 180f);

            // Set visual properties via GraphInfo
            arc.GraphInfo = new GraphInfo
            {
                // Line width of the arc stroke
                LineWidth = 2f,
                // Dash pattern: 5 units on, 3 units off
                DashArray = new int[] { 5, 3 }
                // No FillColor is set, so the arc remains unfilled
            };

            // Add the Arc to the Graph's shape collection
            graph.Shapes.Add(arc);

            // Add the Graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to disk
            doc.Save("ArcExample.pdf");
        }

        Console.WriteLine("PDF with unfilled arc saved as 'ArcExample.pdf'.");
    }
}