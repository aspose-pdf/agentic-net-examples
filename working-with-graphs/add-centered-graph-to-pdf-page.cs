using System;
using System.IO;
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
            // Add a blank page (Pages are 1‑based)
            doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Create a Graph with desired width and height (double literals as required)
            Graph graph = new Graph(300.0, 150.0);

            // Align the graph to the horizontal center of the page
            graph.HorizontalAlignment = HorizontalAlignment.Center;

            // Optionally, set a border or other visual properties via GraphInfo
            // graph.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1f };

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without graphical rendering.");
                    // In many cases the PDF is still saved; if not, you may choose to skip saving.
                }
            }
        }

        Console.WriteLine("PDF with centered graph saved as 'output.pdf'.");
    }

    // Helper to detect nested DllNotFoundException (e.g., missing libgdiplus on Linux/macOS)
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
