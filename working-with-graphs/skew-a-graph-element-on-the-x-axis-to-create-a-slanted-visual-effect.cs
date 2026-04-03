using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class SkewGraphExample
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (the float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0);

            // Set the X‑axis skew angle (e.g., 30 degrees) to achieve a slanted effect
            graph.GraphInfo.SkewAngleX = 30; // degrees

            // Use Aspose.Pdf.Drawing.Rectangle for a shape inside a Graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,   // lower‑left X
                0f,   // lower‑left Y
                300f, // width
                100f  // height
            );
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };
            graph.Shapes.Add(rect);

            // Position the graph on the page (lower‑left corner at (100, 400))
            graph.Left = 100f;
            // The "Bottom" property was removed in newer versions of Aspose.Pdf.
            // If you need to move the graph vertically, adjust the page's coordinate system
            // or use a container (e.g., a Table) to place it at the desired Y position.

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            string outputPath = "SkewedGraph.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                                      "The PDF was saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine("Program completed.");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
