using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace AsposePdfExamples
{
    class AddFilledArcRadialGradient
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple sample PDF (self‑contained example requirement)
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF and add a graph with a gradient‑filled arc
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a graph of desired size
                Graph graph = new Graph(200f, 200f);
                page.Paragraphs.Add(graph);

                // Define an arc (center at 100,100, radius 80, start angle 0°, sweep to 180°)
                Arc arc = new Arc(100f, 100f, 80f, 0f, 180f);

                // Create a radial gradient shading (red at centre, blue at outer edge)
                GradientRadialShading gradient = new GradientRadialShading(Aspose.Pdf.Color.Red, Aspose.Pdf.Color.Blue);
                gradient.Start = new Point(100f, 100f);
                gradient.End = new Point(100f, 100f);
                gradient.StartingRadius = 0f;
                gradient.EndingRadius = 80f;

                // Wrap the gradient in a Color object via its PatternColorSpace property
                Aspose.Pdf.Color fillColor = new Aspose.Pdf.Color();
                fillColor.PatternColorSpace = gradient;

                // Configure graph info for the arc (fill with gradient, outline with black)
                GraphInfo arcInfo = new GraphInfo();
                arcInfo.FillColor = fillColor;
                arcInfo.Color = Aspose.Pdf.Color.Black;
                arcInfo.LineWidth = 1f;
                arc.GraphInfo = arcInfo;

                // Add the arc to the graph
                graph.Shapes.Add(arc);

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
