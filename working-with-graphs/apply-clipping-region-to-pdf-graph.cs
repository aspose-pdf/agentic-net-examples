using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_graph.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (coordinates in points)
            double clipLeft   = 100;
            double clipBottom = 400;
            double clipRight  = 400;
            double clipTop    = 600;

            // Build the clipping path: a rectangle defined by line operators
            page.Contents.Add(new MoveTo(clipLeft,   clipBottom)); // lower‑left
            page.Contents.Add(new LineTo(clipRight,  clipBottom)); // lower‑right
            page.Contents.Add(new LineTo(clipRight,  clipTop));    // upper‑right
            page.Contents.Add(new LineTo(clipLeft,   clipTop));    // upper‑left
            page.Contents.Add(new ClosePath());                    // close the path

            // Apply the clipping region using the non‑zero winding rule
            page.Contents.Add(new Clip());

            // Platform check – Graph rendering (and Document.Save) requires GDI+ (libgdiplus) on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a graph that contains shapes extending beyond the clipping rectangle
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Red rectangle (partially outside the clipping area)
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 350f, 400f, 300f);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(1, 0, 0), // Red fill
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 2f
                };
                graph.Shapes.Add(rectShape);

                // Blue ellipse (partially outside the clipping area)
                var ellipse = new Aspose.Pdf.Drawing.Ellipse(200f, 500f, 300f, 200f);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0, 0, 1), // Blue fill
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 2f
                };
                graph.Shapes.Add(ellipse);

                // Add the graph to the page; only the portion inside the clipping rectangle will be rendered
                page.Paragraphs.Add(graph);

                // End the clipping region
                page.Contents.Add(new EOClip());
            }
            else
            {
                Console.WriteLine("libgdiplus is required for Graph rendering and PDF saving on this platform. Saving PDF without the clipped graph.");
                // End the clipping region even if we didn't add a graph to keep the content stream valid.
                page.Contents.Add(new EOClip());
            }

            // Save the document – guard against missing GDI+ on non‑Windows platforms.
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

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
