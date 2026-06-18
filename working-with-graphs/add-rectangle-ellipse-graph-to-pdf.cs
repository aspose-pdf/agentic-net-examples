using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.Runtime.InteropServices; // optional, for platform‑specific handling

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a graph container with desired size (width, height in points)
            // Use the double‑based constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle shape -----
            // Parameters: left, bottom, width, height (float values are required)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2f // float literal
            };
            // Add rectangle to the graph
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height (float values are required)
            var ellipseShape = new Ellipse(300f, 150f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            // Add ellipse to the graph
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk (guard for platforms without libgdiplus if needed)
            string outputPath = "GraphExample.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // On non‑Windows platforms libgdiplus may be missing; attempt save and handle possible failure gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
            else
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
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
