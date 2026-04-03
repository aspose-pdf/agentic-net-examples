using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) using the double‑based constructor
            Graph graph = new Graph(500.0, 400.0);
            page.Paragraphs.Add(graph);

            // Keep track of occupied rectangles to detect overlaps
            List<Aspose.Pdf.Rectangle> occupied = new List<Aspose.Pdf.Rectangle>();

            // Define rectangles with varying positions and sizes
            var rectDefs = new[]
            {
                new { LLX = 50.0,  LLY = 300.0, Width = 100.0, Height = 80.0 },
                new { LLX = 120.0, LLY = 250.0, Width = 150.0, Height = 100.0 },
                new { LLX = 200.0, LLY = 350.0, Width = 80.0,  Height = 60.0 },
                new { LLX = 300.0, LLY = 200.0, Width = 120.0, Height = 90.0 }
            };

            foreach (var def in rectDefs)
            {
                // Compute upper‑right coordinates for the bounding rectangle
                double urx = def.LLX + def.Width;
                double ury = def.LLY + def.Height;

                // Bounding rectangle used for overlap checking (Aspose.Pdf.Rectangle expects double)
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(def.LLX, def.LLY, urx, ury);

                // Determine if this rectangle intersects any existing one
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
                    // No overlap – create the drawing rectangle (expects float values) and add it to the graph
                    Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)def.LLX,
                        (float)def.LLY,
                        (float)def.Width,
                        (float)def.Height);
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f // float literal
                    };
                    graph.Shapes.Add(shape);

                    // Record its bounds for future checks
                    occupied.Add(bounds);
                }
                else
                {
                    // Overlap detected – skip this rectangle (or handle as needed)
                    Console.WriteLine($"Skipped rectangle at ({def.LLX}, {def.LLY}) due to overlap.");
                }
            }

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the program ran without crashing.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
