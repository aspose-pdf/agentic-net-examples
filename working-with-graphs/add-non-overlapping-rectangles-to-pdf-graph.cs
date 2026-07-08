using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangles_graph.pdf";

        // Create a new PDF document and a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph container that will hold the rectangle shapes
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 400.0); // width, height of the graph canvas

            // List to keep track of placed bounding rectangles for overlap checking
            List<Aspose.Pdf.Rectangle> placedBounds = new List<Aspose.Pdf.Rectangle>();

            // Define a set of rectangle specifications (position and size)
            var rectSpecs = new (double x, double y, double w, double h)[]
            {
                (50, 300, 120, 80),
                (200, 250, 150, 100),
                (400, 320, 80, 60),
                (180, 150, 200, 120),
                (350, 100, 100, 150)
            };

            foreach (var spec in rectSpecs)
            {
                // Create a bounding rectangle for overlap testing (uses double values)
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(
                    spec.x,               // llx
                    spec.y - spec.h,      // lly (PDF Y origin is bottom‑left)
                    spec.x + spec.w,      // urx
                    spec.y                // ury
                );

                // Check against all previously placed rectangles
                bool overlaps = false;
                foreach (var existing in placedBounds)
                {
                    if (bounds.IsIntersect(existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                // If no overlap, add the rectangle to the graph and store its bounds
                if (!overlaps)
                {
                    // Drawing.Rectangle expects float parameters – cast accordingly
                    Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)spec.x,
                        (float)(spec.y - spec.h),
                        (float)spec.w,
                        (float)spec.h);

                    // Set visual appearance via GraphInfo (LineWidth is a float)
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };

                    graph.Shapes.Add(shape);
                    placedBounds.Add(bounds);
                }
                // If overlap occurs, the rectangle is simply skipped (could be logged)
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with non‑overlapping rectangles saved to '{outputPath}'.");
    }
}
