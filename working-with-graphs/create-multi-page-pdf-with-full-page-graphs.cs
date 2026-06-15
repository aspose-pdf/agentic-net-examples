using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

#pragma warning disable NU1903 // suppress known vulnerability warning for Microsoft.Bcl.Memory

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Define page sizes (width, height) in points.
            // A4 = 595 x 842 pt, Letter = 612 x 792 pt, A5 = 420 x 595 pt
            var pageSizes = new (double width, double height)[]
            {
                (595.0, 842.0),   // A4
                (612.0, 792.0),   // Letter
                (420.0, 595.0)    // A5
            };

            foreach (var size in pageSizes)
            {
                // Add a new page and set its size
                Page page = doc.Pages.Add();
                page.SetPageSize(size.width, size.height);

                // Create a Graph that occupies the whole page (double ctor)
                Graph graph = new Graph(size.width, size.height);
                graph.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 1f // float literal as required by GraphInfo
                };

                // Add the graph to the page
                page.Paragraphs.Add(graph);
            }

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                }
                else
                {
                    // Attempt to save; if GDI+ (libgdiplus) is missing, handle gracefully
                    doc.Save(outputPath);
                }
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF was not saved.");
            }
        }
    }

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
