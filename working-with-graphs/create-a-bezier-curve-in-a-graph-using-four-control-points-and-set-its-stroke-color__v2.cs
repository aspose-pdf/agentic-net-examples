using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

#nullable enable

class Program
{
    static void Main()
    {
        const string outputPath = "bezier_curve.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Render the graph only on platforms where GDI+ (libgdiplus) is available (Windows)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a Graph container that covers the page size (Graph expects double values)
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define four control points (start point + three control points)
                // Format: {x0, y0, x1, y1, x2, y2, x3, y3}
                float[] controlPoints = { 100f, 500f, 150f, 600f, 250f, 400f, 300f, 500f };

                // Create the Bezier curve shape and set its stroke color/width
                Curve bezier = new Curve(controlPoints)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Color.Blue, // stroke color
                        LineWidth = 2f
                    }
                };

                // Add the curve to the graph
                graph.Shapes.Add(bezier);

                // Add the graph to the page's content
                page.Paragraphs.Add(graph);
            }
            else
            {
                // On non‑Windows platforms we skip Graph rendering because it depends on GDI+ (libgdiplus).
                // Optionally, add a simple text note.
                page.Paragraphs.Add(new TextFragment("Bezier curve rendering requires GDI+ (libgdiplus) which is unavailable on this platform."));
            }

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bezier curve saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a DllNotFoundException.
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException!; // InnerException may be null; null‑forgiving operator used because loop condition checks for null.
        }
        return false;
    }
}