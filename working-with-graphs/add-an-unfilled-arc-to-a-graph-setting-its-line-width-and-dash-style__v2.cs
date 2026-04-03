using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (as required by newer Aspose.Pdf versions)
            Graph graph = new Graph(400.0, 300.0); // width, height in points

            // Define an unfilled arc:
            // Center at (200,150), radius 100, start angle 0°, end angle 180°
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Set line width (e.g., 2 points) – float literal is accepted for GraphInfo properties
            arc.GraphInfo.LineWidth = 2f;

            // Set dash pattern: 5 points dash, 3 points gap
            arc.GraphInfo.DashArray = new int[] { 5, 3 };

            // No fill color is set – the arc will be unfilled (stroke only)

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
            const string outputPath = "ArcExample.pdf";
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without graph rendering.");
                }
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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