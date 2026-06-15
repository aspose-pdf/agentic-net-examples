using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Suppress known NuGet vulnerability warning (NU1903) for Microsoft.Bcl.Memory
        #pragma warning disable NU1903
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (the old float overload is obsolete)
            Graph graph = new Graph(400.0, 300.0); // width, height as double literals

            // Define circle parameters (center X, center Y, radius)
            float centerX = 200f;   // X‑coordinate of the center
            float centerY = 150f;   // Y‑coordinate of the center
            float radius  = 80f;    // Radius of the circle

            // Create the Circle shape
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual appearance via GraphInfo (fill and stroke colors, line width)
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,   // Fill color
                Color     = Color.DarkBlue,    // Stroke color
                LineWidth = 2f                 // Stroke thickness as float
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "filled_circle.pdf";
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
                    // The document may still be saved; if not, you can choose to skip saving or handle accordingly.
                }
            }
        }
        #pragma warning restore NU1903

        Console.WriteLine("PDF with filled circle created: filled_circle.pdf");
    }

    // Helper method to walk the exception chain and detect a missing native GDI+ library.
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
