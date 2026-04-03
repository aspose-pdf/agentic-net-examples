using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Use the double‑parameter Graph constructor (float overload is obsolete)
            Graph graph = new Graph(500.0, 300.0); // width, height in points

            // Bar chart parameters
            double startX = 50.0;          // left margin
            double startY = 50.0;          // baseline for bars
            double barWidth = 30.0;        // width of each bar
            double barSpacing = 10.0;      // space between bars

            // Sample data values (height of each bar)
            double[] values = { 100, 150, 80, 120, 200, 90, 160, 130, 110, 170 };

            // Loop to create ten rectangles with incremental X coordinates
            for (int i = 0; i < values.Length; i++)
            {
                double x = startX + i * (barWidth + barSpacing);
                double height = values[i];

                // Rectangle expects float arguments – cast accordingly
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,               // left
                    (float)startY,          // bottom
                    (float)barWidth,        // width
                    (float)height);         // height

                // Set visual properties; LineWidth is a float
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightBlue,
                    Color = Aspose.Pdf.Color.DarkBlue,
                    LineWidth = 1f
                };

                // Add rectangle shape to the graph
                graph.Shapes.Add(rect);
            }

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Guard Document.Save on platforms without GDI+ (e.g., macOS/Linux without libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bar chart saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}
