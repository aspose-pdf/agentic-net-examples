using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "translated_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Define an initial rectangle that will serve as the reference
            //    position for the graph element.
            // -----------------------------------------------------------------
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle referenceRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // -----------------------------------------------------------------
            // 2. Create a Graph (container for vector shapes) and add a shape.
            // -----------------------------------------------------------------
            // Use the double‑based Graph constructor as the float overload is obsolete.
            Graph graph = new Graph(200.0, 100.0);

            // Create a drawing rectangle (shape) inside the graph.
            // Drawing.Rectangle expects float parameters.
            var shape = new Aspose.Pdf.Drawing.Rectangle(
                (float)0,
                (float)0,
                (float)200,
                (float)100);
            shape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f // float literal as required by GraphInfo
            };
            graph.Shapes.Add(shape);

            // Position the graph at the lower‑left corner of the reference rectangle.
            graph.Left = referenceRect.LLX;
            graph.Top  = referenceRect.LLY;

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // -----------------------------------------------------------------
            // 3. Translate (move) the reference rectangle horizontally and vertically.
            //    The MoveBy method shifts the rectangle by the specified deltas.
            // -----------------------------------------------------------------
            double deltaX = 50;   // move right by 50 points
            double deltaY = -30;  // move down by 30 points (Y axis grows upwards)
            referenceRect.MoveBy(deltaX, deltaY);

            // Apply the same translation to the graph so it follows the rectangle.
            graph.Left = referenceRect.LLX;
            graph.Top  = referenceRect.LLY;

            // -----------------------------------------------------------------
            // 4. Save the resulting PDF.
            // -----------------------------------------------------------------
            // Guard the Save call on non‑Windows platforms where GDI+ (libgdiplus) may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform; GDI+ may be unavailable. Skipping doc.Save().");
            }
        }
    }
}
