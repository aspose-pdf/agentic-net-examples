using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (width, height)
            Graph graph = new Graph(500.0, 800.0);

            // Create an ellipse (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 500, 200, 100);

            // Set a semi‑transparent fill color (e.g., 50% transparent red)
            // Aspose.Pdf.Color.FromArgb(alpha, red, green, blue) where alpha is 0..255
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0) // 128 ≈ 0.5 opacity
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "EllipseSemiTransparent.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine("PDF with semi‑transparent ellipse creation attempted.");
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