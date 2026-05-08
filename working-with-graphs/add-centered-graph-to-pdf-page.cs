using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_centered.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (as required by newer Aspose.Pdf versions)
            Graph graph = new Graph(200.0, 100.0);

            // Align the graph to the center of the page (both horizontally and vertically)
            graph.HorizontalAlignment = HorizontalAlignment.Center;
            graph.VerticalAlignment   = VerticalAlignment.Center;

            // Define visual appearance via GraphInfo. The border can be set through GraphInfo.Color and LineWidth.
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,   // stroke (border) color
                LineWidth = 1f               // border thickness (float literal)
            };

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                                      "The PDF was generated without rendering the Graph.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
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
