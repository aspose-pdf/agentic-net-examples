using System;
using System.IO;
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

            // Create a Graph container (size of the drawing area)
            // Use the constructor that accepts double values (recommended by Aspose)
            Graph graph = new Graph(400.0, 300.0); // width=400, height=300

            // Define an unfilled arc:
            // Center at (200,150), radius 100, start angle 0°, end angle 180°
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Set line width and dash style via GraphInfo
            arc.GraphInfo = new GraphInfo
            {
                // Line width of 2 points (float literal)
                LineWidth = 2f,
                // Dash pattern: 5 points dash, 3 points gap
                DashArray = new int[] { 5, 3 }
                // No fill color is set, so the arc remains unfilled
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved with Graph rendering.");
                }
            }
        }

        Console.WriteLine("PDF with unfilled arc processing completed.");
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
