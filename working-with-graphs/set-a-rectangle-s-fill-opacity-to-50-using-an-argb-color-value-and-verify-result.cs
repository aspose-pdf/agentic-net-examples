using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rect_opacity.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the double‑parameter constructor for Graph (deprecated float ctor avoided)
            Graph graph = new Graph(400.0, 200.0); // width, height in points (double literals)

            // Define a rectangle – the Drawing.Rectangle ctor expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 150f);

            // Set fill color with 50% opacity using ARGB (alpha = 128)
            var semiTransparentRed = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0); // 0.5 opacity red

            // Configure visual properties via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = semiTransparentRed,   // semi‑transparent fill
                Color = Aspose.Pdf.Color.Black,   // stroke color
                LineWidth = 1f                    // float literal as required
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Verify that the fill color was set correctly
            Aspose.Pdf.Color actualFill = rect.GraphInfo.FillColor;
            bool isCorrect = actualFill.Equals(semiTransparentRed);
            Console.WriteLine($"Fill opacity set correctly: {isCorrect}");

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was created without rendering the graph.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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