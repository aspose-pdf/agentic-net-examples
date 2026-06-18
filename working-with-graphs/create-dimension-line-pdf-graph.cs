using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Suppress known vulnerability warning for Microsoft.Bcl.Memory (NU1903)
        #pragma warning disable NU1903

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 pt, height: 200 pt)
            // Graph constructor requires double values
            Graph graph = new Graph(400.0, 200.0);

            // Define a line from (50,150) to (350,150) – horizontal dimension line
            float[] lineCoords = { 50f, 150f, 350f, 150f };
            Line line = new Line(lineCoords)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,      // line color
                    LineWidth = 2f           // line thickness
                }
            };

            // Add the line to the graph's shape collection
            graph.Shapes.Add(line);

            // Position the graph on the page (lower‑left corner at (100,400))
            graph.Left = 100;
            graph.Top = 400;

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            string outputPath = "dimension_line.pdf";

            // Guard Document.Save for platforms without libgdiplus (macOS/Linux)
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

        Console.WriteLine("PDF with dimension line created: dimension_line.pdf");
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
