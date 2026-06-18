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

            // Use the Graph constructor that accepts double values (width, height)
            Graph graph = new Graph(500.0, 500.0);

            // Define a rectangle using absolute coordinates (float values)
            // left = 100, bottom = 200, width = 150, height = 100
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 200f, 150f, 100f);

            // Set visual properties via GraphInfo – solid red fill
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Red,   // solid fill color
                Color = Aspose.Pdf.Color.Red,       // border color (optional)
                LineWidth = 1f                      // border thickness (float)
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph (which now contains the rectangle) to the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                    // Optionally, you could skip saving the graph or take alternative actions here.
                }
            }
        }

        Console.WriteLine("PDF with red rectangle saved as 'output.pdf'.");
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
