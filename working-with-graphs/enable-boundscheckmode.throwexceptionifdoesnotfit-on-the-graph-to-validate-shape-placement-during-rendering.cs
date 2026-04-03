using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "bounds_check_graph.pdf";
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            if (isWindows)
            {
                // Graph and shape rendering require GDI+ (libgdiplus) which is only available on Windows.
                Graph graph = new Graph(500.0, 400.0);
                // Enable bounds checking – an exception will be thrown if a shape does not fit.
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                                                  graph.Width, graph.Height);
                try
                {
                    var rect = new Aspose.Pdf.Drawing.Rectangle(
                        50.0f,   // left
                        300.0f,  // bottom
                        200.0f,  // width
                        100.0f   // height
                    );
                    rect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };
                    graph.Shapes.Add(rect);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    Console.Error.WriteLine($"Shape does not fit within the graph: {ex.Message}");
                }

                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("Graph rendering requires GDI+ (libgdiplus) which is not available on this platform. Skipping graph creation.");
            }

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
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
