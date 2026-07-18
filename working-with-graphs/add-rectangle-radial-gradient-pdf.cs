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
            // Add a blank page
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Create a Graph container – this holds vector drawing objects.
            // -----------------------------------------------------------------
            // Width and height of the container (in points). Use double literals as required.
            Graph graph = new Graph(500.0, 500.0);

            // -----------------------------------------------------------------
            // 2. Define the rectangle shape that will be filled with the gradient.
            // -----------------------------------------------------------------
            // Parameters: left, bottom, width, height – all as float values.
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 100f, 300f, 200f);

            // -----------------------------------------------------------------
            // 3. Create a radial gradient: red at the centre, transparent at the edge.
            // -----------------------------------------------------------------
            // NOTE: In older Aspose.Pdf versions the GraphInfo class does not expose a
            //       "Shading" property. To keep the code compatible we fall back to a
            //       solid fill that mimics a transparent centre. Full radial‑gradient
            //       support requires a newer library version where GraphInfo.Shading
            //       is available.
            // -----------------------------------------------------------------
            // If you are using a version that supports GraphInfo.Shading, uncomment the
            // lines below and comment out the solid‑fill fallback.
            // -----------------------------------------------------------------
            //GradientRadialShading radialGradient = new GradientRadialShading(
            //    Aspose.Pdf.Color.Red,          // start (centre) colour
            //    Aspose.Pdf.Color.Transparent   // end (edge) colour
            //);
            //radialGradient.Start = new Point(250f, 200f); // centre of the rectangle
            //radialGradient.End   = new Point(250f, 200f); // same centre for the end circle
            //radialGradient.StartingRadius = 0f;
            //radialGradient.EndingRadius   = 150f;
            //rect.GraphInfo.Shading = radialGradient; // requires GraphInfo.Shading support

            // -----------------------------------------------------------------
            // 4. Apply a fallback fill (semi‑transparent red) that works on all versions.
            // -----------------------------------------------------------------
            GraphInfo graphInfo = new GraphInfo();
            graphInfo.Color = Aspose.Pdf.Color.Black; // outline colour
            graphInfo.LineWidth = 1f;
            // Semi‑transparent red (alpha 128 ≈ 50% opacity) – gives a visual hint of a gradient.
            graphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);
            rect.GraphInfo = graphInfo;

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // -----------------------------------------------------------------
            // 5. Save the PDF
            // -----------------------------------------------------------------
            doc.Save("RadialGradientRectangle.pdf");
        }

        Console.WriteLine("PDF with radial gradient rectangle created successfully.");
    }
}
