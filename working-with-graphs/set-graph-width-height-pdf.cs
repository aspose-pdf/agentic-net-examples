using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Graph rendering requires GDI+ (libgdiplus) which is only available on Windows.
            // Guard the creation and addition of the Graph with an OS check.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Initialize a Graph with an initial size (width, height) using double literals as required
                Graph graph = new Graph(200.0, 100.0);

                // Set the exact dimensions of the graph via its properties (double values)
                graph.Width = 300.0;   // Desired width in points
                graph.Height = 150.0;  // Desired height in points

                // Optional: add a rectangle shape to visualize the graph area
                // Use Aspose.Pdf.Drawing.Rectangle and pass float values
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)graph.Width,
                    (float)graph.Height);

                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 1f
                };

                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Non‑Windows platform detected; Graph rendering requires libgdiplus. Skipping graph creation.");
            }

            // Save the PDF document. Guard the call for missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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
