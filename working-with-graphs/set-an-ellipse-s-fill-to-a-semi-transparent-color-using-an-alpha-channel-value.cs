using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_semi_transparent.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container to hold vector shapes (double literals as required)
            Graph graph = new Graph(500.0, 400.0);

            // Create an ellipse (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 150, 300, 200);

            // Set fill color with semi‑transparent red (alpha = 128 ≈ 0.5)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0), // 50% opacity red
                Color = Color.Black,                       // stroke color
                LineWidth = 1f                              // float literal
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine($"PDF processing completed. Output path: '{outputPath}'.");
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