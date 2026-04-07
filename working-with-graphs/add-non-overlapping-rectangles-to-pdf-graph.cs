using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF file
        const string outputPath = "graph_rectangles.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the area where the graph will be placed on the page
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle graphArea = new Aspose.Pdf.Rectangle(50, 300, 550, 700);

            // Create a Graph container with the same size as the graph area (use double literals)
            Graph graph = new Graph(500.0, 400.0);
            page.Paragraphs.Add(graph); // Add the graph to the page

            // Keep track of the bounds of rectangles already placed (in page coordinates)
            List<Aspose.Pdf.Rectangle> existingBounds = new List<Aspose.Pdf.Rectangle>();

            // Define several rectangles (left, bottom, width, height) relative to the graph origin
            var rectSpecs = new (double left, double bottom, double width, double height)[]
            {
                (10, 10, 100, 80),
                (120, 20, 150, 100),
                (200, 150, 120, 90),
                (50, 50, 80, 60),   // This one may overlap with the first rectangle
                (300, 200, 180, 120)
            };

            foreach (var spec in rectSpecs)
            {
                // Create a rectangle shape for the graph (Drawing.Rectangle expects float values)
                Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)spec.left,
                    (float)spec.bottom,
                    (float)spec.width,
                    (float)spec.height);

                // Set visual appearance via GraphInfo (FillColor, Border Color, LineWidth)
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f // float literal
                };

                // Compute the rectangle's bounds in page coordinates (graphArea offset + shape position)
                double llx = graphArea.LLX + spec.left;
                double lly = graphArea.LLY + spec.bottom;
                double urx = llx + spec.width;
                double ury = lly + spec.height;
                Aspose.Pdf.Rectangle pageBounds = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Check for overlap with any previously added rectangle
                bool overlaps = false;
                foreach (var existing in existingBounds)
                {
                    if (pageBounds.IsIntersect(existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    // No overlap – add the shape to the graph and record its bounds
                    graph.Shapes.Add(shapeRect);
                    existingBounds.Add(pageBounds);
                }
                else
                {
                    // Overlap detected – skip this rectangle (or handle as needed)
                    Console.WriteLine($"Skipped rectangle at ({spec.left},{spec.bottom}) due to overlap.");
                }
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with non‑overlapping rectangles saved.");
    }
}
