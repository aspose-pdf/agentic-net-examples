using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Graph constructor uses double literals as required by the API
            Graph graph = new Graph(400.0, 300.0);

            // Create an Arc: center at (200,150), radius 100, start 0°, end 180°
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Define visual appearance: fill color, stroke color, line width
            // Aspose.Pdf.Color.FromRgb expects values in the range 0..1, so we normalise the 0‑255 RGB components.
            arc.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromRgb(173.0 / 255.0, 216.0 / 255.0, 230.0 / 255.0), // Light blue fill
                Color = Aspose.Pdf.Color.Black,                                             // Outline color
                LineWidth = 1f
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. " +
                                  "Saving the PDF requires GDI+ (libgdiplus). " +
                                  "Install libgdiplus or run on Windows to generate the file.");
            }
        }
    }
}
