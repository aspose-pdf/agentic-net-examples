using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the double‑based constructor for Graph (the float overload is obsolete)
            Graph graph = new Graph(200.0, 100.0); // initial size: 200pt x 100pt

            // Set the exact dimensions using the Width and Height properties (double values)
            graph.Width = 300.0;   // desired width in points
            graph.Height = 150.0;  // desired height in points

            // Create a drawing rectangle that matches the graph size.
            // Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) implements Shape and exposes GraphInfo.
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)graph.Width,
                (float)graph.Height);

            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray, // background fill (Aspose.Pdf.Color)
                Color = Color.Black,         // border color
                LineWidth = 1f               // border thickness (float)
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Insert the graph into the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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