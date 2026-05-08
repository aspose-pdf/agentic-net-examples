using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BarChartExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define graph (container for vector shapes)
            // Width and height define the drawing area on the page (in points)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 200.0);

            // Bar chart parameters
            int barCount = 10;
            double barWidth = 30;          // width of each bar
            double barSpacing = 10;        // space between bars
            double maxBarHeight = 150;     // maximum height of a bar
            double baseY = 20;             // bottom margin for bars

            // Loop to create bars with incremental X positions
            for (int i = 0; i < barCount; i++)
            {
                // X coordinate for the current bar
                double x = i * (barWidth + barSpacing);

                // Example: bar height proportional to (i+1)
                double barHeight = ((i + 1) / (double)barCount) * maxBarHeight;

                // Create a rectangle shape (drawing rectangle, not page rectangle)
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,                     // left
                    (float)baseY,                 // bottom
                    (float)barWidth,              // width
                    (float)barHeight);            // height

                // Set visual properties via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.2 + i * 0.08, 0.4, 0.6), // varying fill color
                    Color = Aspose.Pdf.Color.Black,    // border color
                    LineWidth = 1f                     // float literal as required
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(rect);
            }

            // Add the graph (containing all rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "BarChart.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bar chart PDF created: {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Bar chart PDF created (non‑Windows platform): {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf.Drawing.Graph.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException caused by missing libgdiplus
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
