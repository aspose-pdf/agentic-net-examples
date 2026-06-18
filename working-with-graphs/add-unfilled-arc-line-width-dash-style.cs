using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            // Add a new page to the document (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container – width and height define the drawing area
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 400.0);

            // Create an unfilled arc:
            //   center at (250, 200), radius 100, start angle 0°, end angle 180°
            Arc arc = new Arc(250, 200, 100, 0, 180);

            // Configure line width and dash style via GraphInfo (use float literals for line width)
            // DashArray expects an int[] (pattern lengths are integer units)
            arc.GraphInfo = new GraphInfo
            {
                LineWidth = 2f,                     // 2 points line width (float)
                DashArray = new int[] { 5, 3 }      // dash pattern: 5 units on, 3 units off (int[] required)
            };

            // Add the arc shape to the graph
            graph.Shapes.Add(arc);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the PDF file – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save("ArcExample.pdf");
            }
            else
            {
                try
                {
                    doc.Save("ArcExample.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }
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
