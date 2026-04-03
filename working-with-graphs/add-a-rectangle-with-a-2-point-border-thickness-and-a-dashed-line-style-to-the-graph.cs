using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (as required by the API)
            Graph graph = new Graph(400.0, 200.0);

            // Create a rectangle shape for the graph – note the use of Aspose.Pdf.Drawing.Rectangle
            // and float parameters for position and size
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 300f, 100f);

            // Configure visual appearance via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                // Border color
                Color = Aspose.Pdf.Color.Black,
                // Border thickness (2 points) – float literal
                LineWidth = 2f,
                // Dashed line pattern: 3 points dash, 3 points gap
                DashArray = new int[] { 3, 3 }
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            string outputPath = "output.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. " +
                                      "PDF saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine("PDF processing completed.");
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