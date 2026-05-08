using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "graph_embedded.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container (width = 400 points, height = 200 points)
            // NOTE: Graph constructor now expects double values.
            Graph graph = new Graph(400.0, 200.0);

            // ----- Add a filled rectangle -----
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) for vector shapes.
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Add an ellipse -----
            var ellipse = new Ellipse(250f, 50f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Add a line -----
            // Line constructor expects a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100f, 180f, 300f, 180f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the Graph (vector graphic) to the page's paragraph collection.
            // This embeds the vector content directly into the PDF page.
            page.Paragraphs.Add(graph);

            // Save the PDF. Guard the call on non‑Windows platforms where libgdiplus may be missing.
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the vector graphic.");
                }
            }
        }

        Console.WriteLine($"PDF with embedded vector graphic saved to '{outputPath}'.");
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library.
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
