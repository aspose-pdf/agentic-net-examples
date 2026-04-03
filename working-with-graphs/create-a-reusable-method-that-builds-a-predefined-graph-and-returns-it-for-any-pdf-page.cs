using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class GraphFactory
{
    // Builds a reusable Graph containing a rectangle, an ellipse and a line.
    // Width and height define the size of the Graph container.
    public static Graph CreateSampleGraph(double width = 400, double height = 200)
    {
        // Create the Graph container.
        Graph graph = new Graph(width, height);

        // ----- Rectangle -----
        // Constructor: (left, bottom, width, height)
        var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.FromRgb(211, 211, 211), // LightGray
            Color = Color.FromRgb(0, 0, 0),           // Black
            LineWidth = 1f
        };
        graph.Shapes.Add(rect);

        // ----- Ellipse -----
        // Constructor: (left, bottom, width, height)
        var ellipse = new Ellipse(120f, 0f, 80f, 60f);
        ellipse.GraphInfo = new GraphInfo
        {
            FillColor = Color.FromRgb(255, 255, 0), // Yellow
            Color = Color.FromRgb(255, 0, 0),       // Red
            LineWidth = 1.5f
        };
        graph.Shapes.Add(ellipse);

        // ----- Line -----
        // Constructor expects a float array: { x1, y1, x2, y2 }
        float[] linePoints = { 0f, 100f, 300f, 100f };
        var line = new Line(linePoints);
        line.GraphInfo = new GraphInfo
        {
            Color = Color.FromRgb(0, 0, 255), // Blue
            LineWidth = 2f
        };
        graph.Shapes.Add(line);

        return graph;
    }
}

public class Program
{
    private const string OutputPath = "output.pdf";

    public static void Main()
    {
        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Insert the predefined graph into the page **only on platforms where GDI+ is available**.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                page.Paragraphs.Add(GraphFactory.CreateSampleGraph());
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is not available on this platform. Skipping graph insertion.");
            }

            // Save the PDF – guard the call for non‑Windows platforms where libgdiplus may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(OutputPath);
                Console.WriteLine($"PDF saved to '{OutputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(OutputPath);
                    Console.WriteLine($"PDF saved to '{OutputPath}' (no graph rendered).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
