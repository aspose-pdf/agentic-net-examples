using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (size can be larger than the arc)
            // Use the constructor that accepts double values as required by the API
            Graph graph = new Graph(500.0, 500.0);

            // Create an unfilled Arc:
            // Parameters: centerX, centerY, radius, startAngle (alpha), endAngle (beta)
            Arc arc = new Arc(250f, 250f, 100f, 0f, 180f);

            // Set line width (float)
            arc.GraphInfo.LineWidth = 2f;

            // Set dash style (e.g., 3 units dash, 2 units gap)
            arc.GraphInfo.DashArray = new int[] { 3, 2 };

            // No fill color is set, so the arc remains unfilled

            // Add the Arc to the Graph
            graph.Shapes.Add(arc);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "ArcExample.pdf";
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Helper to detect a nested DllNotFoundException
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