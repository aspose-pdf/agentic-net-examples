using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Platform‑specific handling – Graph (and therefore GDI+) is only guaranteed on Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container (width and height of the page)
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define ellipse position and size
                double ellipseLeft   = 100;   // points from the left edge
                double ellipseBottom = 400;   // points from the bottom edge
                double ellipseWidth  = 200;   // ellipse width
                double ellipseHeight = 150;   // ellipse height

                // Create the ellipse
                Ellipse ellipse = new Ellipse(ellipseLeft, ellipseBottom, ellipseWidth, ellipseHeight);

                // Apply a solid fill color (gradient fill cannot be assigned directly to FillColor)
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,      // stroke color
                    LineWidth = 1f
                };

                // Add the ellipse to the graph
                graph.Shapes.Add(ellipse);

                // Add the graph to the page
                page.Paragraphs.Add(graph);

                // Measure the bounding box of the ellipse
                double bboxLeft   = ellipse.Left;
                double bboxBottom = ellipse.Bottom;
                double bboxRight  = ellipse.Left + ellipse.Width;
                double bboxTop    = ellipse.Bottom + ellipse.Height;

                Console.WriteLine("Ellipse Bounding Box:");
                Console.WriteLine($"  Left   : {bboxLeft}");
                Console.WriteLine($"  Bottom : {bboxBottom}");
                Console.WriteLine($"  Right  : {bboxRight}");
                Console.WriteLine($"  Top    : {bboxTop}");
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform. Skipping ellipse drawing.");
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms.
            string outputPath = "EllipseWithBoundingBox.pdf";
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
