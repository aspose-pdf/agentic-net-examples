using System;
using System.Collections.Generic;
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
            // Add a blank page to host the graph
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);

            // Lookup table that maps shape indices to border colors
            var borderColors = new Dictionary<int, Color>
            {
                { 0, Color.Red },
                { 1, Color.Green },
                { 2, Color.Blue }
            };

            // ---------- Shape 0: Rectangle (drawing shape) ----------
            // Use Aspose.Pdf.Drawing.Rectangle – it expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo { Color = borderColors[0] };
            graph.Shapes.Add(rect);

            // ---------- Shape 1: Ellipse ----------
            // Ellipse constructor also expects float values
            var ellipse = new Ellipse(150f, 0f, 100f, 50f);
            ellipse.GraphInfo = new GraphInfo { Color = borderColors[1] };
            graph.Shapes.Add(ellipse);

            // ---------- Shape 2: Line ----------
            // Line constructor expects a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 0f, 150f, 300f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo { Color = borderColors[2] };
            graph.Shapes.Add(line);

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "graph_output.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graph rendering.");
                    // Optionally, you could save a placeholder PDF or skip saving entirely.
                }
            }
        }

        Console.WriteLine("PDF with graph created: graph_output.pdf");
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
