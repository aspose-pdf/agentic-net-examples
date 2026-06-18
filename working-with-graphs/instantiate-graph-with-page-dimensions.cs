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
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Retrieve the page dimensions (width and height in points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph object that matches the page size (double constructor)
            Graph graph = new Graph(pageWidth, pageHeight);

            // OPTIONAL: add a sample rectangle shape to the graph
            // Rectangle constructor: (left, bottom, width, height) – uses float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)100,
                (float)(pageHeight - 200),
                (float)200,
                (float)100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS/Linux the Graph rendering may require libgdiplus. Attempt to save and handle the possible failure gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform)" );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper method to walk the exception chain and detect a missing native GDI+ library.
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
