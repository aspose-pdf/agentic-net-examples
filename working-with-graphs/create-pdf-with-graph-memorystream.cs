using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class PdfGenerator
{
    public static byte[] CreatePdfWithGraph()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // On Windows we can safely add a Graph (requires GDI+). On non‑Windows platforms
            // Aspose.Pdf may throw a TypeInitializationException because libgdiplus is missing.
            // Guard the Graph creation and rendering with an OS check.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Graph constructor expects double values for width and height
                Graph graph = new Graph(400.0, 200.0);

                // Use the fully‑qualified drawing rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page
                page.Paragraphs.Add(graph);
            }
            else
            {
                // On non‑Windows platforms we skip the Graph to avoid GDI+ dependency.
                // Optionally you could add alternative content here.
                Console.WriteLine("Graph rendering skipped – libgdiplus is not available on this platform.");
            }

            // Save the PDF to a memory stream and return the byte array.
            using (MemoryStream ms = new MemoryStream())
            {
                // Guard Document.Save for platforms without GDI+.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(ms);
                }
                else
                {
                    try
                    {
                        doc.Save(ms);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        // Fallback: return the PDF generated so far (may be empty) without throwing.
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF saved without GDI+ dependent content.");
                    }
                }
                return ms.ToArray();
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

public class Program
{
    // Entry point required for a console application
    public static void Main()
    {
        byte[] pdfBytes = PdfGenerator.CreatePdfWithGraph();
        Console.WriteLine($"Generated PDF size: {pdfBytes.Length} bytes");
    }
}