using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (the old float overload is obsolete)
            Graph graph = new Graph(400.0, 400.0);

            // Define the six vertices of a regular hexagon (center at (200,200), radius 100)
            float[][] vertices = new float[][]
            {
                new float[] { 300f, 200f }, // V0
                new float[] { 250f, 286.6f }, // V1
                new float[] { 150f, 286.6f }, // V2
                new float[] { 100f, 200f }, // V3
                new float[] { 150f, 113.4f }, // V4
                new float[] { 250f, 113.4f }  // V5
            };

            // Fully qualify Path to avoid ambiguity with System.IO.Path
            Aspose.Pdf.Drawing.Path hexagonPath = new Aspose.Pdf.Drawing.Path();

            // Add six lines connecting the vertices (V0→V1, V1→V2, ..., V5→V0)
            for (int i = 0; i < vertices.Length; i++)
            {
                float[] start = vertices[i];
                float[] end = vertices[(i + 1) % vertices.Length]; // wrap to first vertex

                // Line constructor expects an array: { x1, y1, x2, y2 }
                float[] lineCoords = { start[0], start[1], end[0], end[1] };
                Line edge = new Line(lineCoords);

                // Add the line to the Path
                hexagonPath.Shapes.Add(edge);
            }

            // Set border color and thickness for the whole path
            hexagonPath.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // border color
                LineWidth = 2f                    // border thickness
            };

            // Add the completed path to the graph
            graph.Shapes.Add(hexagonPath);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "HexagonGraph.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved with graphical content.");
                }
            }
        }

        Console.WriteLine("PDF with a hexagon graph saved as 'HexagonGraph.pdf'.");
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
