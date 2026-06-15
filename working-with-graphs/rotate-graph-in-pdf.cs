using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values
            Graph graph = new Graph(400.0, 200.0);

            // Apply a rotation to the entire graph (degrees). Use double literal as required.
            graph.GraphInfo.RotationAngle = 45.0;

            // Create a rectangle shape for the graph.
            // Constructor parameters: lower‑left X, lower‑left Y, width, height (all float).
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // llx
                50f,   // lly
                100f,  // width  (150 - 50)
                50f);  // height (100 - 50)

            // Configure visual appearance using GraphInfo and Aspose.Pdf.Color
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            string outputPath = "rotated_graph.pdf";

            // Guard Document.Save for platforms where libgdiplus (GDI+) is unavailable (e.g., macOS/Linux).
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus) inside a TypeInitializationException.
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
