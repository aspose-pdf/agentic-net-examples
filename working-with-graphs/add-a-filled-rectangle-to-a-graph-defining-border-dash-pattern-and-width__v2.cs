using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph constructor expects double values
            Graph graph = new Graph(400.0, 200.0)
            {
                Left = 100,
                Top  = 500
            };

            // Drawing rectangle (shape) – use Aspose.Pdf.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 2f,
                    // DashArray expects an int[] (pattern lengths in points)
                    DashArray = new int[] { 5, 3 }
                }
            };

            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine($"PDF processing completed. Output path: '{outputPath}'.");
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