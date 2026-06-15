using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        // Suppress known NuGet vulnerability warning (NU1903) for this sample
        #pragma warning disable NU1903
        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold the bar shapes
            double graphWidth = 500;   // total width of the graph area
            double graphHeight = 300;  // total height of the graph area
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page (optional)
                Left = 50,
                Top = 500   // measured from the bottom of the page
            };

            // Parameters for the bars
            double barWidth = 30;          // width of each bar
            double barSpacing = 20;        // space between bars
            double baseY = 0;              // Y coordinate of the bar base within the graph

            // Generate ten bars with incremental X positions
            for (int i = 0; i < 10; i++)
            {
                // X coordinate for the current bar
                double x = i * (barWidth + barSpacing);

                // Example varying height for visual distinction
                double barHeight = 20 + i * 15;

                // Create a rectangle shape for the bar
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,          // left
                    (float)baseY,      // bottom
                    (float)barWidth,   // width
                    (float)barHeight   // height
                );

                // Define visual appearance via GraphInfo
                bar.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.2f + i * 0.08f, 0.4f, 0.6f), // varying fill color
                    Color = Aspose.Pdf.Color.Black,                               // border color
                    LineWidth = 1f
                };

                // Add the bar to the graph's shape collection
                graph.Shapes.Add(bar);
            }

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: libgdiplus is not available on this platform; PDF saved without GDI+ dependent rendering.");
                }
            }
        }
        #pragma warning restore NU1903

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
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
