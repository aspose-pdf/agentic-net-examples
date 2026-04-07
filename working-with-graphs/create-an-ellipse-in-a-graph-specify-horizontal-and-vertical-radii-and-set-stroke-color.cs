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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (avoid obsolete float overload)
            Graph graph = new Graph(400.0, 300.0); // width, height as double literals
            page.Paragraphs.Add(graph); // Place the graph on the page

            // Define horizontal and vertical radii
            double horizontalRadius = 100; // radius along the X‑axis
            double verticalRadius   = 50;  // radius along the Y‑axis

            // Calculate left and bottom positions for the ellipse
            double left   = 150; // X‑coordinate of the left side
            double bottom = 200; // Y‑coordinate of the bottom side

            // Create the ellipse (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(left, bottom, horizontalRadius * 2, verticalRadius * 2);

            // Set the stroke (outline) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red // stroke color
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            string outputPath = "ellipse_output.pdf";

            // Guard Document.Save on platforms where GDI+ (libgdiplus) may be missing
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
                                      "The PDF was created without Graph rendering.");
                }
            }
        }
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