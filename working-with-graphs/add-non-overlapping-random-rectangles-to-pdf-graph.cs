using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "rectangles_graph.pdf";

        // Page dimensions (default A4 size in points)
        const double pageWidth  = 595; // 8.27 inches * 72
        const double pageHeight = 842; // 11.69 inches * 72

        // Number of rectangles to attempt to place
        const int rectangleCount = 10;

        // Random generator for sizes and positions
        Random rnd = new Random();

        // List to keep track of occupied bounds (Aspose.Pdf.Rectangle)
        List<Aspose.Pdf.Rectangle> occupied = new List<Aspose.Pdf.Rectangle>();

        // Create a new PDF document (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph container (used for drawing vector shapes)
            Graph graph = new Graph(pageWidth, pageHeight);

            for (int i = 0; i < rectangleCount; i++)
            {
                // Random width/height between 50 and 150 points
                double width  = rnd.Next(50, 151);
                double height = rnd.Next(50, 151);

                // Random lower‑left corner ensuring the rectangle stays inside the page
                double llx = rnd.NextDouble() * (pageWidth  - width);
                double lly = rnd.NextDouble() * (pageHeight - height);
                double urx = llx + width;
                double ury = lly + height;

                // Create a PDF rectangle representing the bounds of this shape
                Aspose.Pdf.Rectangle bounds = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Check for overlap with any previously placed rectangle
                bool overlaps = false;
                foreach (var existing in occupied)
                {
                    if (bounds.IsIntersect(existing))
                    {
                        overlaps = true;
                        break;
                    }
                }

                // If there is no overlap, add the shape to the graph
                if (!overlaps)
                {
                    // Drawing.Rectangle uses (left, bottom, width, height) constructor – expects float values
                    Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)llx,
                        (float)lly,
                        (float)width,
                        (float)height);

                    // Set visual appearance via GraphInfo
                    shape.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.FromRgb(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble()),
                        Color     = Aspose.Pdf.Color.Black,
                        LineWidth = 1f // float literal
                    };

                    // Add shape to the graph
                    graph.Shapes.Add(shape);

                    // Record its bounds for future overlap checks
                    occupied.Add(bounds);
                }
                // If overlap occurs, simply skip this rectangle (could retry with new random values)
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).);");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. " +
                                      "The PDF was not saved, but the program executed without crashing.");
                }
            }
        }
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
