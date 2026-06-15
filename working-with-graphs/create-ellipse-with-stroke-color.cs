using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Define ellipse radii
        double horizontalRadius = 100; // radius along X axis
        double verticalRadius   = 60;  // radius along Y axis

        // Calculate bounding box for the ellipse
        double left   = 200;                       // X coordinate of left side
        double bottom = 300;                       // Y coordinate of bottom side
        double width  = horizontalRadius * 2;       // total width = 2 * horizontal radius
        double height = verticalRadius * 2;         // total height = 2 * vertical radius

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size can be larger than the ellipse)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 500.0);

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set stroke (outline) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,          // stroke color
                LineWidth = 1f               // optional: explicit line width as float
                // FillColor can be set here as well if needed
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF (guarded for non‑Windows platforms where GDI+ may be missing)
            try
            {
                doc.Save("ellipse_output.pdf");
                Console.WriteLine("Ellipse PDF created: ellipse_output.pdf");
            }
            catch (System.TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus on Linux/macOS)
    private static bool ContainsDllNotFound(Exception ex)
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
