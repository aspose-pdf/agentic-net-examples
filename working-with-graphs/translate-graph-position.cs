using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class TranslateGraphExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph (container for drawing shapes)
            // Width = 400 points, Height = 200 points – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0)
            {
                // Initial position of the graph on the page
                Left = 100,   // X coordinate (points from the left edge)
                Top  = 500    // Y coordinate (points from the bottom edge)
            };

            // Create a rectangle shape inside the graph
            // Parameters: left, bottom, width, height (relative to the graph)
            // Rectangle constructor expects float values
            var shape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            shape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2f   // float literal as required
            };
            graph.Shapes.Add(shape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Translate (move) the graph horizontally and vertically
            // ------------------------------------------------------------
            double dx = 50;   // shift right by 50 points
            double dy = -30;  // shift down by 30 points (negative Y moves down)

            // Update the graph's position
            graph.Left += dx;
            graph.Top  += dy;

            // Save the PDF to a file – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "translated_graph.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'. Graph moved by ({dx}, {dy}) points.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save because GDI+ (libgdiplus) is not available on this platform.");
                Console.WriteLine($"Graph would have been moved by ({dx}, {dy}) points.");
            }
        }
    }
}
