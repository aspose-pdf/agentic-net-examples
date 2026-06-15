using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (as required by the API)
            Graph graph = new Graph(400.0, 300.0); // width, height in points

            // Create an Arc: center at (200,150), radius 100, from 0° to 180°
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Define visual appearance: fill color, stroke color, line width (float literals)
            arc.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0.4f, 0.7f, 1.0f), // light blue fill
                Color = Color.Black,                       // black outline
                LineWidth = 2f
            };

            // Add the arc to the graph's shape collection
            graph.Shapes.Add(arc);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where GDI+ (libgdiplus) may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved may be incomplete.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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