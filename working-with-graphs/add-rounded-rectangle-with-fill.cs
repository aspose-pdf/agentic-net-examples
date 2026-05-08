using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "RoundedRectangle.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the size of the drawing canvas (graph)
            // Here we use the same size as the page for simplicity
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            page.Paragraphs.Add(graph);

            // Create a rectangle shape with rounded corners
            // Constructor parameters: left, bottom, width, height (float values)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f)
            {
                RoundedCornerRadius = 20f // radius of the corners
            };

            // Set solid fill color (e.g., LightGray) and stroke
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black, // stroke color (optional)
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rectShape);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated but could not be saved using Graph rendering.");
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