using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define graph dimensions (use page size for simplicity)
            double graphWidth  = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;

            // Create a Graph object using the double overload (recommended)
            Graph graph = new Graph(graphWidth, graphHeight);

            // -------------------------------------------------
            // 1) Filled rectangle (stroke + fill)
            // -------------------------------------------------
            // Position: 100, 500 (lower‑left corner), size 200x100 points
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // Fill with light gray
                Color     = Color.Black,       // Stroke (border) color
                LineWidth = 2f                 // Stroke thickness
            };
            graph.Shapes.Add(filledRect);

            // -------------------------------------------------
            // 2) Outline‑only rectangle (stroke without fill)
            // -------------------------------------------------
            // Position: 350, 500, size 200x100 points
            var outlineRect = new Aspose.Pdf.Drawing.Rectangle(350, 500, 200, 100);
            outlineRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Transparent, // No fill
                Color     = Color.Red,         // Stroke color
                LineWidth = 2f                 // Stroke thickness
            };
            graph.Shapes.Add(outlineRect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document.
            // On non‑Windows platforms Aspose.Pdf may require GDI+ (libgdiplus);
            // guard the save operation to avoid a TypeInitializationException.
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
                                      "The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException
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