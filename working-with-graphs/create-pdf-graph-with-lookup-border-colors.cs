using System;
using System.Collections.Generic;
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
            Page page = doc.Pages.Add();

            // Graph constructor expects double values
            Graph graph = new Graph(400.0, 300.0);
            graph.Left = 50;
            graph.Top = 500;

            var colorLookup = new Dictionary<string, Color>
            {
                { "rect", Color.FromRgb(1, 0, 0) },      // Red border
                { "ellipse", Color.FromRgb(0, 1, 0) },   // Green border
                { "line", Color.FromRgb(0, 0, 1) }       // Blue border
            };

            // ---------- Rectangle ----------
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                Color = colorLookup["rect"],
                LineWidth = 2f,
                FillColor = Color.FromRgb(0.9, 0.9, 0.9)   // Light gray fill
            };
            graph.Shapes.Add(rect);

            // ---------- Ellipse ----------
            var ellipse = new Ellipse(200f, 0f, 350f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                Color = colorLookup["ellipse"],
                LineWidth = 2f,
                FillColor = Color.FromRgb(0.8, 0.8, 1)     // Light blue fill
            };
            graph.Shapes.Add(ellipse);

            // ---------- Line ----------
            float[] linePoints = { 0f, 150f, 350f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = colorLookup["line"],
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
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
