using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Graph rendering (rectangle) requires GDI+ (libgdiplus on non‑Windows).
            // Guard it so the code runs on any platform.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph that covers the whole page
                Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

                // Define a rectangle with absolute coordinates:
                // left = 100, bottom = 500, width = 200, height = 100
                var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

                // Set a solid red fill (and optional border color)
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Red,
                    Color = Aspose.Pdf.Color.Red
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is not available on this platform. Skipping graph creation.");
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                // Optionally, you could re‑throw, log, or handle the situation differently.
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a DllNotFoundException.
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
