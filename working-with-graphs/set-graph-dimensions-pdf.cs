using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values (the float overload is obsolete)
            Graph graph = new Graph(200.0, 100.0);

            // Set the exact dimensions using the Width and Height properties (double values)
            graph.Width = 300.0;   // Desired width in points
            graph.Height = 150.0;  // Desired height in points

            // Rectangle constructor expects float parameters – cast the graph dimensions
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)graph.Width,
                (float)graph.Height);
            rect.GraphInfo = new GraphInfo { FillColor = Aspose.Pdf.Color.LightGray };
            graph.Shapes.Add(rect);

            // Insert the graph into the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
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
                    // Optionally, you could save a placeholder PDF or skip saving entirely.
                }
            }
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
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
