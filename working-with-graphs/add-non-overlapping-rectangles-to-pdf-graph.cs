using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Path to output PDF
        const string outputPath = "rectangles_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define graph dimensions (same as page size for simplicity)
            double graphWidth = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;

            // Create a Graph container to hold drawing shapes (double ctor required)
            Graph graph = new Graph(graphWidth, graphHeight);

            // List to keep track of rectangle bounds for overlap checking
            List<Aspose.Pdf.Rectangle> existingBounds = new List<Aspose.Pdf.Rectangle>();

            // Define rectangle specifications (left, bottom, width, height)
            var rectSpecs = new (double left, double bottom, double width, double height)[]
            {
                (50, 700, 150, 80),
                (250, 650, 120, 100),
                (400, 720, 180, 60),
                (100, 500, 200, 150),
                (350, 400, 130, 120)
            };

            foreach (var spec in rectSpecs)
            {
                // Create a bounding rectangle for overlap detection (uses double coordinates)
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(
                    spec.left,
                    spec.bottom,
                    spec.left + spec.width,
                    spec.bottom + spec.height);

                // Check against all existing rectangles
                bool overlaps = false;
                foreach (var existing in existingBounds)
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
                    // Create a drawing rectangle shape – parameters must be float
                    var shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)spec.left,
                        (float)spec.bottom,
                        (float)spec.width,
                        (float)spec.height);

                    // Set visual appearance via GraphInfo (LineWidth is float)
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };

                    // Add shape to the graph
                    graph.Shapes.Add(shape);

                    // Store bounds for future overlap checks
                    existingBounds.Add(bounds);
                }
                else
                {
                    // Overlap detected – you could handle it differently (e.g., log or adjust)
                    Console.WriteLine($"Rectangle at ({spec.left},{spec.bottom}) overlaps and was skipped.");
                }
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"Process completed. PDF path: '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
