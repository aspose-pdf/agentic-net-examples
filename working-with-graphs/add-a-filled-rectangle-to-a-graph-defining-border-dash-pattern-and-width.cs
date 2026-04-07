using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // On Windows we can render the Graph (requires GDI+). On other platforms we skip it.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a graph that covers the whole page – Graph constructor expects double values
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define a rectangle shape (left, bottom, width, height) – Rectangle expects float values
                var shapeRect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

                // Set fill color, border color, line width, and dash pattern via GraphInfo
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,   // Fill color
                    Color = Aspose.Pdf.Color.Blue,           // Border color
                    LineWidth = 2f,                          // Border width
                    DashArray = new int[] { 5, 3 }           // Dash pattern: 5 units on, 3 units off
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(shapeRect);

                // Add the graph to the page
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform – the graph will be omitted.");
            }

            // Save the PDF – guard the call for platforms without GDI+
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
