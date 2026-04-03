using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_rectangle.pdf";

        // Create a new PDF document inside a using‑statement so it is disposed correctly
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Graph rendering (which internally uses GDI+) is only supported on Windows platforms.
            // On macOS / Linux the native libgdiplus library is required; if it is missing the
            // call to Document.Save will throw a TypeInitializationException. We therefore guard
            // the graph creation and the Save call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Graph constructor requires double values for width and height
                Graph graph = new Graph(500.0, 300.0);

                // Create a rectangle shape for the graph – Aspose.Pdf.Drawing.Rectangle expects
                // float values: left, bottom, width, height
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    50f,   // left
                    50f,   // bottom
                    200f,  // width
                    100f   // height
                );

                // Apply visual styling via GraphInfo. LineWidth is a float.
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray, // background fill
                    Color = Color.Black,         // border color
                    LineWidth = 2f               // border thickness
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);

                // Position the graph on the page – Left and Top are double properties
                graph.Left = 50.0;   // distance from the left edge of the page
                graph.Top = 400.0;   // distance from the bottom edge of the page

                // Add the graph (which now contains the rectangle) to the page
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is not available on this platform. Skipping graph creation.");
            }

            // Save the PDF document – guard the call for platforms where libgdiplus is missing.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Helper method that walks the inner‑exception chain to detect a DllNotFoundException.
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
