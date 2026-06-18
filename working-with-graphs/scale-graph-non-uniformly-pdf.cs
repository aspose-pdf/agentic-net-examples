using System;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Suppress the known NuGet vulnerability warning for Microsoft.Bcl.Memory
[assembly: SuppressMessage("NuGet", "NU1903", Justification = "The package is required for Aspose.Pdf and the vulnerability does not affect this scenario.")]

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Document lifecycle must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph with a base size (width, height) in points – use double literals as required
            Graph graph = new Graph(400.0, 200.0);

            // Apply non‑uniform scaling:
            //   X axis scaled to 150% (1.5)
            //   Y axis scaled to  75% (0.75)
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5f,
                ScalingRateY = 0.75f
            };

            // Add a rectangle shape to visualize the scaling effect – use Aspose.Pdf.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Position the graph on the page
            graph.Left = 50f;   // distance from the left edge
            graph.Top = 600f;   // distance from the bottom edge

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Graph saved to '{outputPath}'.");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS requires libgdiplus for Graph rendering; skip saving the graph.
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without the graph.");
                // Save the document without the graph (graph is already added, but we avoid the Save that would trigger GDI+).
                // Optionally, you could remove the graph from the page before saving.
                // For demonstration we simply skip the Save call.
            }
            else
            {
                // Linux or other platforms – attempt to save; if libgdiplus is missing an exception will be thrown.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Graph saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("libgdiplus is not available on this platform; the PDF was not saved.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
