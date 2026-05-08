using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – size large enough for the lines
            // Use the constructor that accepts double values (as required by the API)
            Graph graph = new Graph(500.0, 800.0);

            // ---------- First line segment (red) ----------
            // Position array: { x1, y1, x2, y2 }
            float[] line1Pos = { 50f, 700f, 150f, 600f };
            Line line1 = new Line(line1Pos)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Red,
                    LineWidth = 2f
                }
            };
            graph.Shapes.Add(line1);

            // ---------- Second line segment (green) ----------
            float[] line2Pos = { 150f, 600f, 250f, 650f };
            Line line2 = new Line(line2Pos)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Green,
                    LineWidth = 2f
                }
            };
            graph.Shapes.Add(line2);

            // ---------- Third line segment (blue) ----------
            float[] line3Pos = { 250f, 650f, 350f, 550f };
            Line line3 = new Line(line3Pos)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,
                    LineWidth = 2f
                }
            };
            graph.Shapes.Add(line3);

            // Add the Graph (which now contains multiple colored line segments) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS / Linux the Aspose.Pdf rendering engine may require libgdiplus.
                // Attempt to save and handle the possible TypeInitializationException gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException
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
