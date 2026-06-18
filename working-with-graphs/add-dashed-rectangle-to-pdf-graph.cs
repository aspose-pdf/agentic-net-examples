using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (the old float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0)
            {
                Left = 50,   // X position on the page
                Top  = 500   // Y position on the page
            };

            // Create a rectangle shape for the graph. Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            // and pass float values for the dimensions.
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);

            // Configure the rectangle's border: 2‑point thickness, black color, dashed pattern
            rect.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Black,   // border color
                LineWidth = 2f,                       // border thickness (points) – float literal
                DashArray = new int[] { 3, 3 }        // dash‑gap pattern (dashed line)
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to a file – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform, libgdiplus may be required.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. The PDF was not saved.");
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
