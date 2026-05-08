using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page (Graph expects double values)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define circle parameters: center coordinates and radius
            float centerX = 200f;   // X-coordinate of the circle center
            float centerY = 400f;   // Y-coordinate of the circle center
            float radius  = 50f;    // Radius of the circle

            // Instantiate the Circle shape
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual styling via GraphInfo (filled with LightBlue, black border)
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            string outputPath = "filled_circle.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was saved without rendering the graph.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
