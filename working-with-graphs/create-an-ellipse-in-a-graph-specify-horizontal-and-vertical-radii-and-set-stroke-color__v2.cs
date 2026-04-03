using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Parameters for the ellipse
        double centerX = 200;          // X coordinate of the ellipse centre
        double centerY = 400;          // Y coordinate of the ellipse centre
        double horizontalRadius = 80;  // Radius along the X‑axis
        double verticalRadius   = 50;  // Radius along the Y‑axis

        // Compute the bounding rectangle for the ellipse
        double left   = centerX - horizontalRadius;
        double bottom = centerY - verticalRadius;
        double width  = horizontalRadius * 2;
        double height = verticalRadius   * 2;

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Graph rendering (requires GDI+/libgdiplus). Guard it on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container – size can be the page size or larger
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Create the ellipse shape with the calculated bounds
                Ellipse ellipse = new Ellipse(left, bottom, width, height)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Red,   // stroke color
                        LineWidth = 2f                 // thickness of the stroke (float literal)
                    }
                };

                // Add the ellipse to the graph
                graph.Shapes.Add(ellipse);

                // Add the graph to the page's content
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform. Skipping shape creation.");
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "EllipseExample.pdf";
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Unable to save PDF because GDI+ (libgdiplus) is missing on this platform.");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for DllNotFoundException
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