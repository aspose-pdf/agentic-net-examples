using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

#nullable enable

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Number of pages to generate
            int pageCount = 3;

            // Determine if we are on a platform that supports GDI+ (required for Graph rendering & Document.Save)
            bool canRenderGraph = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default size is A4)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (width and height in points)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                if (canRenderGraph)
                {
                    // Create a Graph that exactly matches the page size
                    Graph graph = new Graph(pageWidth, pageHeight);

                    // Optional: define visual appearance via GraphInfo
                    graph.GraphInfo = new GraphInfo
                    {
                        FillColor = Color.LightGray, // background fill
                        Color     = Color.Black,    // border color
                        LineWidth = 1                // border thickness
                    };

                    // Add the graph to the page's paragraph collection
                    page.Paragraphs.Add(graph);
                }
                else
                {
                    // On non‑Windows platforms we cannot render the Graph (requires libgdiplus).
                    // Add a simple text placeholder so the page is not empty.
                    page.Paragraphs.Add(new TextFragment($"Page {i} – Graph rendering requires GDI+ (libgdiplus) which is unavailable on this platform."));
                }
            }

            // Save the PDF – guard the call for platforms without GDI+.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF was generated but could not be saved using Aspose.Pdf's default renderer.");
                // As a fallback, attempt to save using the PDF/A 1b format which does not require GDI+ for simple documents.
                try
                {
                    doc.Save(outputPath, SaveFormat.Pdf);
                    Console.WriteLine($"PDF saved (fallback) to '{outputPath}'.");
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"Failed to save PDF even with fallback: {fallbackEx.Message}");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (libgdiplus).
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
