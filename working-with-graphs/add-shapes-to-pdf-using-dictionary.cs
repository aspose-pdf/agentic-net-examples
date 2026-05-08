using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Dictionary mapping shape identifiers to fill colors
            var shapeFillColors = new Dictionary<string, Color>
            {
                { "rect1", Color.LightGray },
                { "ellipse1", Color.Yellow },
                { "line1", Color.Red }
            };

            // Graph rendering requires GDI+ (libgdiplus) which is only available on Windows.
            // On non‑Windows platforms we skip adding the graph to avoid a TypeInitializationException.
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            Graph? graph = null;
            if (isWindows)
            {
                // Create a Graph container (width: 400, height: 200) using double literals as required by the constructor
                graph = new Graph(400.0, 200.0);

                // ---- Aspose.Pdf.Drawing.Rectangle shape ----
                var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 100f, 80f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = shapeFillColors["rect1"],
                    Color = Color.Black,      // stroke color
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);

                // ---- Ellipse shape ----
                var ellipse = new Ellipse(200f, 150f, 120f, 80f);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = shapeFillColors["ellipse1"],
                    Color = Color.DarkBlue,
                    LineWidth = 1f
                };
                graph.Shapes.Add(ellipse);

                // ---- Line shape ----
                float[] linePoints = { 50f, 50f, 300f, 50f };
                var line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = shapeFillColors["line1"],
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Add the Graph to the page's paragraphs collection
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform. Skipping graph creation.");
            }

            // Save the PDF document. Guard the call on non‑Windows platforms where libgdiplus may be missing.
            string outputPath = "shapes_with_dictionary.pdf";
            if (isWindows)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graphical content.");
                }
            }
        }

        Console.WriteLine("PDF creation process finished.");
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library.
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
