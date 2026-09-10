using System;
using System.Collections.Generic;
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

            // Create a Graph container that matches the page size (Graph constructor accepts double values)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Keep track of rectangles that have already been placed
            List<Aspose.Pdf.Rectangle> placedBounds = new List<Aspose.Pdf.Rectangle>();

            // Define a set of rectangles with varying positions and sizes
            var rectSpecs = new (double llx, double lly, double width, double height)[]
            {
                (100, 500, 150, 80),
                (200, 400, 120, 100),
                (300, 600, 200, 150),
                (120, 520, 80, 60),
                (400, 300, 100, 200)
            };

            // Iterate over each rectangle specification
            foreach (var spec in rectSpecs)
            {
                // Compute the upper‑right coordinates
                double urx = spec.llx + spec.width;
                double ury = spec.lly + spec.height;

                // Create a bounding rectangle for overlap checking (Aspose.Pdf.Rectangle uses double)
                Aspose.Pdf.Rectangle candidateBounds = new Aspose.Pdf.Rectangle(spec.llx, spec.lly, urx, ury);

                // Check against all previously placed rectangles
                bool overlaps = false;
                foreach (var existing in placedBounds)
                {
                    if (candidateBounds.IsIntersect(existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                // If no overlap, add the rectangle shape to the graph
                if (!overlaps)
                {
                    // Drawing.Rectangle constructor expects float values (left, bottom, width, height)
                    Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)spec.llx,
                        (float)spec.lly,
                        (float)spec.width,
                        (float)spec.height);

                    // Set visual appearance via GraphInfo (LineWidth is a float)
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };

                    // Add shape to the graph and record its bounds
                    graph.Shapes.Add(shape);
                    placedBounds.Add(candidateBounds);
                }
            }

            // Add the graph (containing all non‑overlapping rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }
    }
}
