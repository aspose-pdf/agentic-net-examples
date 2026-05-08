using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (avoid obsolete float overload)
            Graph graph = new Graph(400.0, 300.0); // width, height of the canvas

            // Define ellipse parameters
            double left = 100;                 // X coordinate of the left side of the bounding box
            double bottom = 100;               // Y coordinate of the bottom side of the bounding box
            double horizontalRadius = 80;      // Desired horizontal radius
            double verticalRadius = 50;        // Desired vertical radius
            double width = horizontalRadius * 2;   // Width of the bounding box
            double height = verticalRadius * 2;    // Height of the bounding box

            // Create the ellipse shape using the bounding box dimensions
            Ellipse ellipse = new Ellipse(left, bottom, width, height)
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Red   // Stroke color
                    // FillColor can be set here if a fill is required
                }
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph (containing the ellipse) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Ellipse PDF saved to '{outputPath}'.");
                }
                else
                {
                    // On Linux/macOS the native GDI+ library (libgdiplus) may be absent.
                    // Either install libgdiplus or skip saving here.
                    Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
                }
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ native library is missing; the PDF could not be saved on this platform.");
            }
        }
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