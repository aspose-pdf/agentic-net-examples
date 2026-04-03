using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define clipping rectangle coordinates (left, bottom, right, top)
            float left = 100f;
            float bottom = 400f;
            float right = 300f;
            float top = 600f;

            // Build the clipping path – a rectangle defined by line operators
            page.Contents.Add(new MoveTo(left, bottom));
            page.Contents.Add(new LineTo(right, bottom));
            page.Contents.Add(new LineTo(right, top));
            page.Contents.Add(new LineTo(left, top));
            page.Contents.Add(new ClosePath());

            // Apply the clipping region using the non‑zero winding rule (W operator)
            page.Contents.Add(new Clip());

            // Create a Graph container and add shapes that will be drawn
            // Graph constructor expects double values for width and height
            Graph graph = new Graph(400.0, 200.0);

            // Rectangle shape that exceeds the clipping bounds (will be clipped)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 400f, 300f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // Diagonal line shape (also exceeds the clipping bounds)
            float[] linePoints = { 0f, 0f, 500f, 500f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page – it will be rendered only inside the clipping rectangle
            page.Paragraphs.Add(graph);

            // End the clipping region using the even‑odd rule (W* operator)
            page.Contents.Add(new EOClip());

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "clipped_graph.pdf";
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
                    // GDI+ (libgdiplus) is not available – remove the Graph (which needs GDI+) and save again
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. Saving PDF without the Graph rendering.");
                    page.Paragraphs.Remove(graph);
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved (without Graph) to '{outputPath}'.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a DllNotFoundException (libgdiplus)
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
