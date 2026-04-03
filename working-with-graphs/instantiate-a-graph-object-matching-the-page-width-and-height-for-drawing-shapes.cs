using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Retrieve the page dimensions (width and height in points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Graph rendering depends on GDI+ (libgdiplus). Guard it on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Instantiate a Graph object that matches the page size (double overload)
                Graph graph = new Graph(pageWidth, pageHeight);

                // Sample rectangle shape to demonstrate usage
                var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 100f, 200f, 150f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 1f // float literal as required by GraphInfo
                };
                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires libgdiplus on non‑Windows platforms. Skipping graph creation.");
            }

            // Save the PDF document – guard the call for platforms without GDI+.
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (graph omitted).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graph rendering.");
                }
            }
        }
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library.
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
