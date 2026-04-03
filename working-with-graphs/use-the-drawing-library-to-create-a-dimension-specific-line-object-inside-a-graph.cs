using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Use the double‑based Graph constructor (float overload is obsolete)
            Graph graph = new Graph(500.0, 200.0);

            // Define a line from (100,100) to (400,100) – Line expects a float[]
            float[] linePositions = { 100f, 100f, 400f, 100f };
            Line line = new Line(linePositions)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,
                    LineWidth = 2f
                }
            };

            graph.Shapes.Add(line);

            // Position the graph on the page (points)
            graph.Left = 50;   // distance from left edge
            graph.Top  = 600;  // distance from bottom edge

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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

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
