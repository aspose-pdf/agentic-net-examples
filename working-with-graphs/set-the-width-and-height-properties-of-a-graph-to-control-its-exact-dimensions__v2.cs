using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Graph rendering depends on GDI+ (libgdiplus). Guard it on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Use the Graph constructor that accepts double values (width, height in points)
                Graph graph = new Graph(400.0, 200.0);
                // Width/Height can also be set explicitly (float values)
                graph.Width = 400f;
                graph.Height = 200f;

                // Create a drawing rectangle (Aspose.Pdf.Drawing.Rectangle) and set its GraphInfo
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 400f, 200f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f // float as required
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires libgdiplus on non‑Windows platforms; the graph will be omitted.");
            }

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a DllNotFoundException
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
