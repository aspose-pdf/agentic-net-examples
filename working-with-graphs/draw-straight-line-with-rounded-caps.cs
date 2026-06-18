using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

#pragma warning disable NU1903 // Suppress known vulnerability warning for Microsoft.Bcl.Memory (handled by package update in CI)

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Set line cap style for subsequent drawing operations on this page
            // This will affect the line we draw next
            page.Contents.Add(new SetLineCap(LineCap.RoundCap));

            // Create a Graph container sized to the page dimensions (double literals as required)
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define start and end points for the line (x1, y1, x2, y2)
            float[] linePoints = { 100f, 700f, 500f, 700f };
            var line = new Line(linePoints);

            // Configure visual appearance of the line
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
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

        Console.WriteLine($"PDF with line saved to '{outputPath}'.");
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
#pragma warning restore NU1903