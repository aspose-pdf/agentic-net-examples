using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";
        const int totalPages = 3; // adjust as needed

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            // Create the required number of pages
            for (int i = 1; i <= totalPages; i++)
            {
                // Add a new page (1‑based indexing)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (width & height in points)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                if (isWindows)
                {
                    // Create a Graph that matches the page size (Windows platforms have GDI+ support)
                    Graph graph = new Graph(pageWidth, pageHeight)
                    {
                        Left = 0,   // position at the left edge
                        Top  = 0    // position at the top edge
                    };
                    page.Paragraphs.Add(graph);
                }
                else
                {
                    // On non‑Windows platforms GDI+ (libgdiplus) may be missing; add a placeholder text instead.
                    page.Paragraphs.Add(new TextFragment($"Page {i}: graph omitted on non‑Windows platform"));
                }
            }

            // Save the document – guard the call for platforms without GDI+ support.
            if (isWindows)
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (no Graphs rendered).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform; PDF not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
