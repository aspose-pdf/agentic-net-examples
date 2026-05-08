using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "dimension_line.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // On non‑Windows platforms Graph rendering requires libgdiplus. Guard it.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a graph container (size: 400x200 points) – use double literals as required
                Graph graph = new Graph(400.0, 200.0);

                // Define a line from (50,150) to (350,150)
                float[] linePositions = { 50f, 150f, 350f, 150f };
                Line line = new Line(linePositions);

                // Style the line (red color, 2‑point width)
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 2f
                };

                // Add the line to the graph
                graph.Shapes.Add(line);

                // Position the graph on the page (optional)
                graph.Left = 0f;   // X‑coordinate of the graph's lower‑left corner
                graph.Top = 0f;    // Y‑coordinate of the graph's lower‑left corner

                // Add the graph to the page
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on non‑Windows platforms. Skipping graph creation.");
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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
