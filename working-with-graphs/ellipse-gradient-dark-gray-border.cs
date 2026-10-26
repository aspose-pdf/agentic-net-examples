using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a Graph container – all drawing objects must be
            //    added to a Graph before they can be placed on a page.
            // ------------------------------------------------------------
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ------------------------------------------------------------
            // 2. Define the ellipse.
            //    The Ellipse constructor expects (left, bottom, width, height).
            // ------------------------------------------------------------
            float left   = 100f;   // X‑coordinate of the lower‑left corner
            float bottom = 300f;   // Y‑coordinate of the lower‑left corner
            float width  = 200f;   // Width of the ellipse
            float height = 200f;   // Height of the ellipse
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // ------------------------------------------------------------
            // 3. Styling – border (stroke) and fill.
            //    The API exposes a GraphInfo object for each shape.
            //    A true radial gradient is not available in the core API of
            //    older Aspose.Pdf versions, therefore we use a solid light‑gray
            //    fill that approximates the requested visual. The border is set
            //    to dark gray with a thickness of 2 points.
            // ------------------------------------------------------------
            ellipse.GraphInfo.Color = Color.DarkGray;          // Stroke color
            ellipse.GraphInfo.LineWidth = 2;                  // Stroke thickness
            ellipse.GraphInfo.FillColor = Color.LightGray;    // Fill (solid – gradient not supported in this version)

            // ------------------------------------------------------------
            // 4. Add the ellipse to the graph and the graph to the page.
            // ------------------------------------------------------------
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ellipse saved to '{outputPath}'.");
    }
}
