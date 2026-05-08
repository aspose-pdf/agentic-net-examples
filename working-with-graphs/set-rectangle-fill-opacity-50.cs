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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page
            // (Graph rendering requires GDI+, which is only available on Windows)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

            // Create a semi‑transparent red color (alpha = 128 ≈ 50% opacity)
            Color semiTransparentRed = Color.FromArgb(128, 255, 0, 0);

            // Set visual properties via GraphInfo (fill color with opacity, stroke color, line width)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = semiTransparentRed,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus is missing
            const string outputPath = "rectangle_opacity.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on non‑Windows platforms. " +
                                  "Skipping doc.Save() to avoid TypeInitializationException.");
            }

            // Verify the fill color (display its ARGB representation)
            Color fill = rect.GraphInfo.FillColor;
            Console.WriteLine($"Fill Color ARGB -> {fill}");
        }
    }
}
