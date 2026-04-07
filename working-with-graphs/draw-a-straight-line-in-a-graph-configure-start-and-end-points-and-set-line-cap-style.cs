using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Set the line cap style (e.g., RoundCap) for subsequent drawing operations
            page.Contents.Add(new SetLineCap(LineCap.RoundCap));

            // Graph rendering requires GDI+ (libgdiplus) on non‑Windows platforms.
            // Render the graph only when the platform provides GDI+ (Windows) to avoid runtime crashes.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a graph container (width, height) using the double‑based constructor
                Graph graph = new Graph(400.0, 200.0);

                // Define start (x1, y1) and end (x2, y2) points of the line
                float[] linePoints = { 50f, 50f, 350f, 150f };
                Line line = new Line(linePoints)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Color.Red,
                        LineWidth = 3f // float literal as required by GraphInfo
                    }
                };

                // Add the line shape to the graph
                graph.Shapes.Add(line);

                // Add the graph to the page's content
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Skipping Graph rendering on non‑Windows platform (requires libgdiplus).");
            }

            // Save the resulting PDF – guard the call for platforms without GDI+
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (graph omitted).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF saved without graph rendering.");
                }
            }
        }
    }

    // Helper to walk the inner‑exception chain and detect a missing native GDI+ library
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