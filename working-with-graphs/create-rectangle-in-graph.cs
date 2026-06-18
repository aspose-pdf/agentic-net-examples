using System;
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
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (required by newer API)
            Graph graph = new Graph(400.0, 200.0); // 400pt wide, 200pt high

            // Define a rectangle shape with exact dimensions inside the graph
            // Rectangle constructor expects float values for coordinates and size
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);

            // Style the rectangle using GraphInfo (fill color, border color, line width)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f // float literal as required
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Insert the graph (which now contains the rectangle) into the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF document – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "dimension_rectangle.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved may be incomplete.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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