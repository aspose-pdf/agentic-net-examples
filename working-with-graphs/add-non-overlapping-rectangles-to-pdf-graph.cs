using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangles.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the rectangles
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold all rectangle shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Store bounds of rectangles that have been added (used for overlap checking)
            List<Aspose.Pdf.Rectangle> existingRects = new List<Aspose.Pdf.Rectangle>();

            // Define rectangle specifications (position and size)
            var specs = new[]
            {
                new { X = 50.0,  Y = 700.0, Width = 100.0, Height = 50.0 },
                new { X = 200.0, Y = 650.0, Width = 120.0, Height = 80.0 },
                new { X = 150.0, Y = 720.0, Width = 80.0,  Height = 40.0 },
                new { X = 300.0, Y = 600.0, Width = 150.0, Height = 100.0 },
                new { X = 100.0, Y = 500.0, Width = 200.0, Height = 150.0 }
            };

            foreach (var s in specs)
            {
                // Compute rectangle bounds (PDF coordinate system: origin at bottom‑left)
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(
                    s.X,                 // lower‑left X (LLX)
                    s.Y - s.Height,      // lower‑left Y (LLY)
                    s.X + s.Width,       // upper‑right X (URX)
                    s.Y);                // upper‑right Y (URY)

                // Verify that the new rectangle does not overlap any existing one
                bool overlaps = false;
                foreach (var existing in existingRects)
                {
                    if (RectanglesOverlap(bounds, existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (overlaps)
                {
                    // Skip this rectangle because it would intersect an existing one
                    continue;
                }

                // Create the drawing rectangle (left, bottom, width, height)
                float left   = (float)bounds.LLX;
                float bottom = (float)bounds.LLY;
                float width  = (float)(bounds.URX - bounds.LLX);
                float height = (float)(bounds.URY - bounds.LLY);

                Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height);

                // Set visual properties via GraphInfo
                shape.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;   // interior fill
                shape.GraphInfo.Color     = Aspose.Pdf.Color.DarkGray;   // border color
                shape.GraphInfo.LineWidth = 1;

                // Add the shape to the graph
                graph.Shapes.Add(shape);

                // Record its bounds for subsequent overlap checks
                existingRects.Add(bounds);
            }

            // Add the graph (which now contains all non‑overlapping rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with non‑overlapping rectangles saved to '{outputPath}'.");
    }

    // Determines whether two rectangles intersect
    static bool RectanglesOverlap(Aspose.Pdf.Rectangle a, Aspose.Pdf.Rectangle b)
    {
        // No overlap if one rectangle is completely to the left/right or above/below the other
        bool noOverlap = a.URX <= b.LLX || a.LLX >= b.URX || a.URY <= b.LLY || a.LLY >= b.URY;
        return !noOverlap;
    }
}
