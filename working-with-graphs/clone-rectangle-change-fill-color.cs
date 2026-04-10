using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (as required by the library)
            Graph graph = new Graph(500.0, 400.0); // width, height of the canvas

            // Define the original rectangle parameters (float literals as required by Aspose.Pdf.Drawing.Rectangle)
            float left = 50f;
            float bottom = 300f;
            float rectWidth = 200f;
            float rectHeight = 150f;

            // Create the original rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(left, bottom, rectWidth, rectHeight);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            // Add the original rectangle to the graph
            graph.Shapes.Add(rect);

            // Clone the rectangle by re‑using the original parameters and shifting it to the right
            var clonedRect = new Aspose.Pdf.Drawing.Rectangle(
                left + 250f,   // new left (LLX) position
                bottom,        // same bottom (LLY) position
                rectWidth,     // same width
                rectHeight);   // same height
            clonedRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Black,
                LineWidth = 1f
            };
            // Add the cloned rectangle to the same graph
            graph.Shapes.Add(clonedRect);

            // Attach the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform, GDI+ may be missing but save succeeded.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a DllNotFoundException (e.g., missing libgdiplus)
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
