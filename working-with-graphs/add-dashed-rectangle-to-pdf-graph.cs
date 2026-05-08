using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document (self‑contained, no external input file required)
        using (Document doc = new Document())
        {
            // Add a blank page to host the vector graphics
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double overload as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape (Aspose.Pdf.Drawing.Rectangle, not Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                LineWidth = 2f,                     // 2‑point border thickness
                Color = Aspose.Pdf.Color.Black,     // black border for visibility
                DashArray = new int[] { 3, 3 }      // dashed style: 3‑pt dash, 3‑pt gap
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Place the graph on the page
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
                                      "The PDF was not saved with the graph rendering.");
                }
            }
        }

        Console.WriteLine($"Rectangle with dashed 2‑pt border processing completed. Output path: '{outputPath}'.");
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library
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
