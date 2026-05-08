using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_with_shadow.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define rectangle dimensions
            float left   = 100f;   // X coordinate
            float bottom = 500f;   // Y coordinate
            float width  = 200f;
            float height = 100f;

            // Offset for the shadow (e.g., 5 points right and down)
            float shadowOffsetX = 5f;
            float shadowOffsetY = -5f; // negative because PDF Y axis goes up

            // Create a Graph container (size large enough to hold both shapes)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(
                (double)width + Math.Abs(shadowOffsetX) + 20.0,
                (double)height + Math.Abs(shadowOffsetY) + 20.0);

            // ---- Shadow rectangle (drawn first, semi‑transparent) ----
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                left + shadowOffsetX,
                bottom + shadowOffsetY,
                width,
                height);

            // Set visual properties for the shadow using an ARGB color (50% opacity)
            shadowRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 179, 179, 179), // 50% transparent light gray
                Color = Color.Gray,
                LineWidth = 1f
            };

            // Add shadow shape to the graph
            graph.Shapes.Add(shadowRect);

            // ---- Main rectangle (drawn on top) ----
            var mainRect = new Aspose.Pdf.Drawing.Rectangle(
                left,
                bottom,
                width,
                height);

            // Set visual properties for the main rectangle
            mainRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.DarkBlue,
                LineWidth = 2f
            };

            // Add main shape to the graph
            graph.Shapes.Add(mainRect);

            // Add the graph (containing both rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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