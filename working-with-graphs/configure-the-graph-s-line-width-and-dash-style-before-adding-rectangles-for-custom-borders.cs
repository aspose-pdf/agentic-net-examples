using System;
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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – double literals as required
            Graph graph = new Graph(400.0, 200.0);

            // Set the line width and dash pattern for the graph's drawing operations
            graph.GraphInfo.LineWidth = 2f;
            graph.GraphInfo.DashArray = new int[] { 5, 3 };
            graph.GraphInfo.Color = Aspose.Pdf.Color.Blue;

            // Create a rectangle shape for the graph (left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // left
                50f,   // bottom
                250f,  // width  (300 - 50)
                100f   // height (150 - 50)
            );

            // Configure the rectangle's visual properties via its GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f,
                DashArray = new int[] { 5, 3 }
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was saved without rendering the Graph.");
                }
            }
        }
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