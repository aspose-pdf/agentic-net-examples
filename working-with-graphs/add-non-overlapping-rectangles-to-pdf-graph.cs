using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the graph
            Page page = doc.Pages.Add();

            // Graph container (width, height) – acts as a canvas for vector shapes
            // Use the double‑based constructor (the float overload is obsolete)
            Graph graph = new Graph(500.0, 500.0);

            // Keep track of occupied page‑level rectangles to detect overlaps
            List<Aspose.Pdf.Rectangle> occupied = new List<Aspose.Pdf.Rectangle>();

            // Define rectangles with varying positions and sizes
            var rectSpecs = new[]
            {
                new { LLX = 50.0,  LLY = 400.0, Width = 100.0, Height = 80.0 },
                new { LLX = 200.0, LLY = 350.0, Width = 150.0, Height = 120.0 },
                new { LLX = 120.0, LLY = 200.0, Width = 80.0,  Height = 60.0 },
                new { LLX = 300.0, LLY = 100.0, Width = 120.0, Height = 90.0 }
            };

            foreach (var spec in rectSpecs)
            {
                // Build a page‑level rectangle for bounds checking (double constructor is fine)
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(
                    spec.LLX,
                    spec.LLY,
                    spec.LLX + spec.Width,
                    spec.LLY + spec.Height);

                // Verify that the new rectangle does not intersect any existing one
                bool overlaps = false;
                foreach (var existing in occupied)
                {
                    if (bounds.IsIntersect(existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    // Record the rectangle as occupied
                    occupied.Add(bounds);

                    // Create the drawing rectangle (shape) – the constructor expects float values
                    var shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)spec.LLX,
                        (float)spec.LLY,
                        (float)spec.Width,
                        (float)spec.Height);

                    // Set visual appearance via GraphInfo (FillColor, Border Color, LineWidth)
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f // float literal
                    };

                    // Add the shape to the graph
                    graph.Shapes.Add(shape);
                }
                else
                {
                    // Overlap detected – skip or handle as needed
                    Console.WriteLine($"Skipped overlapping rectangle at ({spec.LLX}, {spec.LLY}).");
                }
            }

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Persist the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}
