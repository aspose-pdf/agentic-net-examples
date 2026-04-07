using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page (use double literals as required)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the circle's center coordinates and radius
            float centerX = 200f;   // X‑coordinate of the center
            float centerY = 400f;   // Y‑coordinate of the center
            float radius  = 50f;    // Radius of the circle

            // Instantiate the Circle shape and set its visual appearance
            Circle circle = new Circle(centerX, centerY, radius)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightBlue,   // Fill the circle with light blue
                    Color     = Color.DarkBlue,    // Outline color
                    LineWidth = 2f                  // Outline thickness (float literal)
                }
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library
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
