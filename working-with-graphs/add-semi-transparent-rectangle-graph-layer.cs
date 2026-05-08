using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "transparency_demo.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Begin a transparency layer by using a Graph container.
            // The Graph acts as a drawing canvas where we can place shapes.
            // -----------------------------------------------------------------
            // Graph constructor takes width and height (double values)
            Graph graph = new Graph(500.0, 400.0);
            // Position the graph on the page (optional)
            graph.Left = 50;   // X coordinate of the graph's lower‑left corner
            graph.Top  = 500;  // Y coordinate of the graph's lower‑left corner

            // -----------------------------------------------------------------
            // Create a semi‑transparent rectangle shape.
            // -----------------------------------------------------------------
            // Rectangle constructor: (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);

            // Set visual properties via GraphInfo.
            // FillColor is semi‑transparent blue (alpha 0.5). Aspose.Pdf.Color
            // supports alpha via the FromArgb method.
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255), // 50% transparent blue
                Color     = Aspose.Pdf.Color.Black,                  // stroke color
                LineWidth = 1.0f
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // -----------------------------------------------------------------
            // End the transparency layer by adding the graph to the page.
            // -----------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Optional: flatten transparency if you need rasterized content
            // doc.FlattenTransparency();

            // Save the document. Guard the save on non‑Windows platforms
            // because Graph rendering may require GDI+ (libgdiplus).
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping save on non‑Windows platform (GDI+ may be unavailable).");
            }
        }
    }
}
