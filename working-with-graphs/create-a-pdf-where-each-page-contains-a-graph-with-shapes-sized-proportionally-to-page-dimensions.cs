using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_pages.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Define how many pages we want.
            int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default size is A4).
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (points). 1 point = 1/72 inch.
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a Graph that fills the whole page. Graph constructor accepts double values.
                Graph graph = new Graph(pageWidth, pageHeight);

                // -------------------- Rectangle shape --------------------
                // Example: a rectangle that occupies 50% width and 30% height,
                // positioned at 10% from left and 10% from bottom.
                float rectX = (float)(pageWidth  * 0.1);
                float rectY = (float)(pageHeight * 0.1);
                float rectW = (float)(pageWidth  * 0.5);
                float rectH = (float)(pageHeight * 0.3);

                var rect = new Aspose.Pdf.Drawing.Rectangle(rectX, rectY, rectW, rectH);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 1f // float literal as required by GraphInfo
                };
                graph.Shapes.Add(rect);

                // -------------------- Ellipse shape --------------------
                // Example: an ellipse that occupies 30% width and 20% height,
                // centered in the page.
                double ellipseWidth  = pageWidth  * 0.3;
                double ellipseHeight = pageHeight * 0.2;
                double ellipseX      = (pageWidth  - ellipseWidth)  / 2;
                double ellipseY      = (pageHeight - ellipseHeight) / 2;

                Ellipse ellipse = new Ellipse(ellipseX, ellipseY, ellipseWidth, ellipseHeight);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color     = Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // -------------------- Line shape --------------------
                // Example: a diagonal line from top‑left to bottom‑right,
                // spanning 80% of the page width.
                float[] linePoints =
                {
                    (float)(pageWidth  * 0.1), (float)(pageHeight * 0.9), // start point
                    (float)(pageWidth  * 0.9), (float)(pageHeight * 0.1)  // end point
                };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color     = Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection.
                page.Paragraphs.Add(graph);
            }

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with graphs saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (graph rendering may be limited on this platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved with graphical content.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
