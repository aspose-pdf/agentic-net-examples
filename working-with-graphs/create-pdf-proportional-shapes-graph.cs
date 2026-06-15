using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const int pageCount = 3;               // Number of pages to create
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // Retrieve page dimensions (points)
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a Graph that fills the entire page (double literals as required)
                Graph graph = new Graph(pageWidth, pageHeight);

                // ---------- Rectangle (80% width, 30% height) ----------
                float rectWidth = (float)(0.8 * pageWidth);
                float rectHeight = (float)(0.3 * pageHeight);
                // Position: left = 0, top = pageHeight - rectHeight (origin is bottom‑left)
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    (float)(pageHeight - rectHeight),
                    rectWidth,
                    rectHeight);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);

                // ---------- Ellipse (centered, 40% of page size) ----------
                float ellipseWidth = (float)(0.4 * pageWidth);
                float ellipseHeight = (float)(0.4 * pageHeight);
                float ellipseX = (float)((pageWidth - ellipseWidth) / 2);
                float ellipseY = (float)((pageHeight - ellipseHeight) / 2);
                var ellipse = new Aspose.Pdf.Drawing.Ellipse(
                    ellipseX,
                    ellipseY,
                    ellipseWidth,
                    ellipseHeight);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(ellipse);

                // ---------- Diagonal line ----------
                float[] linePoints = { 0f, 0f, (float)pageWidth, (float)pageHeight };
                var line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page
                page.Paragraphs.Add(graph);
            }

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF created: {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF created (non‑Windows platform): {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. " +
                                      "The PDF was generated without rendering the Graph objects.");
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
