using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_pages.pdf";

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            int pageCount = 3; // Number of pages to generate

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a fresh page
                Page page = doc.Pages.Add();

                // Retrieve page dimensions (points)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Define graph size as a proportion of the page size (e.g., 80%)
                double graphWidth  = pageWidth  * 0.8;
                double graphHeight = pageHeight * 0.8;

                // Create a Graph container with the calculated size (Graph ctor accepts double)
                Graph graph = new Graph(graphWidth, graphHeight)
                {
                    // Center the graph on the page
                    Left = (pageWidth  - graphWidth)  / 2,
                    Top  = (pageHeight - graphHeight) / 2
                };

                // ----- Add a rectangle (50% width, 30% height of the graph) -----
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)(graphWidth * 0.25),   // left
                    (float)(graphHeight * 0.35),  // bottom
                    (float)(graphWidth * 0.5),    // width
                    (float)(graphHeight * 0.3)    // height
                );
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);

                // ----- Add an ellipse that fits inside the rectangle -----
                var ellipse = new Aspose.Pdf.Drawing.Ellipse(
                    (float)(graphWidth * 0.25),
                    (float)(graphHeight * 0.35),
                    (float)(graphWidth * 0.5),
                    (float)(graphHeight * 0.3)
                );
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color     = Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // ----- Add a diagonal line across the entire graph -----
                float[] linePoints = {
                    0f, 0f,
                    (float)graphWidth, (float)graphHeight
                };
                var line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color     = Color.Blue,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Attach the graph to the page
                page.Paragraphs.Add(graph);
            }

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
