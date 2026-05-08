using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (width, height)
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle shape (drawing rectangle) -----
            // Position (left=50, bottom=200), size (width=150, height=100)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 200f, 150f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // Fill with light gray
                Color = Aspose.Pdf.Color.Black,           // Stroke color
                LineWidth = 1f                            // Stroke width (float)
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Position (left=250, bottom=150), size (width=120, height=80)
            var ellipseShape = new Ellipse(250f, 150f, 120f, 80f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,      // Fill with yellow
                Color = Aspose.Pdf.Color.Red,             // Stroke color
                LineWidth = 2f                            // Stroke width (float)
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Graph rendering may be incomplete on this platform.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved with graph rendering.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library
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
