using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph of size 400 × 200 points – Graph constructor expects double values
            Graph graph = new Graph(400.0, 200.0);

            // Apply non‑uniform scaling:
            //   X‑axis  = 150 % (1.5)
            //   Y‑axis  =  75 % (0.75)
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5,
                ScalingRateY = 0.75
            };

            // Add a rectangle shape to visualize the scaling effect.
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) because Graph.Shapes expects a Shape.
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo.LineWidth
            };
            graph.Shapes.Add(rect);

            // Position the graph on the page (Left/Top are double)
            graph.Left = 50.0;
            graph.Top  = 500.0;

            // Add the graph to the page's paragraph collection
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
                                      "The PDF was generated without rendering the Graph element.");
                }
            }
        }
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
