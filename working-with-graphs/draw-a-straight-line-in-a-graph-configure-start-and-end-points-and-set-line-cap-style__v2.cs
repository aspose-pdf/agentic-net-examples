using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Determine if we are running on Windows (GDI+ is available)
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                // Set the line cap style for subsequent drawing operations
                page.Contents.Add(new SetLineCap(LineCap.RoundCap));

                // Create a Graph container (width, height) to hold vector shapes
                Graph graph = new Graph(500.0, 200.0);

                // Define start and end points of the line: {x1, y1, x2, y2}
                float[] linePoints = { 50f, 100f, 450f, 100f };
                Line line = new Line(linePoints);

                // Configure visual properties of the line via GraphInfo
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,   // line color
                    LineWidth = 3f                    // line thickness
                };

                // Add the line shape to the graph
                graph.Shapes.Add(line);

                // Add the graph to the page's content
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform; Graph rendering requires libgdiplus which is not available. Skipping graph creation.");
            }

            // Save the PDF to a file – guard the call for platforms without GDI+
            string outputPath = "line_graph.pdf";
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
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
