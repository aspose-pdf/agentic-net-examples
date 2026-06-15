using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height in points) – use double literals as required by the API
            Graph graph = new Graph(500.0, 400.0);
            graph.Left = 50f;   // X position on the page
            graph.Top = 500f;   // Y position (from bottom)

            // -------- Filled rectangle --------
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // Fill color
                Color = Aspose.Pdf.Color.DarkBlue,      // Stroke color
                LineWidth = 2f
            };
            graph.Shapes.Add(filledRect);

            // -------- Outline‑only rectangle (stroke only) --------
            var outlineRect = new Aspose.Pdf.Drawing.Rectangle(250f, 0f, 200f, 100f);
            outlineRect.GraphInfo = new GraphInfo
            {
                // No FillColor => transparent fill
                Color = Aspose.Pdf.Color.Red,   // Stroke color
                LineWidth = 3f
            };
            graph.Shapes.Add(outlineRect);

            // -------- Filled ellipse --------
            var filledEllipse = new Ellipse(0f, 150f, 150f, 100f);
            filledEllipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Orange,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(filledEllipse);

            // -------- Outline‑only ellipse (stroke only) --------
            var outlineEllipse = new Ellipse(200f, 150f, 150f, 100f);
            outlineEllipse.GraphInfo = new GraphInfo
            {
                // No FillColor => transparent fill
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f
            };
            graph.Shapes.Add(outlineEllipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with graph saved to '{outputPath}'. (Non‑Windows platform, libgdiplus present.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. " +
                                      "The PDF was created without rendering the Graph shapes.");
                    // Optionally, you could save a PDF without the graph or take other fallback actions here.
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
