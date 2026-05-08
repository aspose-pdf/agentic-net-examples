using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_border.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as required by the API
            Graph graph = new Graph(500.0, 300.0);

            // Configure line width and dash pattern for shapes
            GraphInfo shapeInfo = new GraphInfo
            {
                LineWidth = 2f,                     // Set line width (float)
                DashArray = new int[] { 5, 3 },     // 5 units dash, 3 units gap
                Color = Color.Blue                  // Set stroke color
            };

            // Create a rectangle shape (left, bottom, width, height) – use Aspose.Pdf.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rect.GraphInfo = shapeInfo; // Apply line width and dash style

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                }
            }
        }

        Console.WriteLine($"PDF processing completed. Check '{outputPath}' if it was saved.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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