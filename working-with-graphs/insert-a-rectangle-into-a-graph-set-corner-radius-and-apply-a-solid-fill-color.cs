using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a graph that covers the whole page
            double graphWidth = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;
            Graph graph = new Graph(graphWidth, graphHeight);
            page.Paragraphs.Add(graph);

            // Define rectangle position and size (left, bottom, width, height)
            float left   = 100f;
            float bottom = 500f;
            float width  = 200f;
            float height = 100f;

            // Create the rectangle shape
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height);

            // Set corner radius
            rect.RoundedCornerRadius = 15f;

            // Apply a solid fill color
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; the graph cannot be rendered.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}