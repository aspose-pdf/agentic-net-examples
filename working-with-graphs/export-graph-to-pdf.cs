using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ExportGraphExample
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Graph rendering requires GDI+ (libgdiplus). Render only on Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Graph graph = new Graph(400.0, 200.0); // double literals as required
                graph.Title = new TextFragment("Sample Graph");
                page.Paragraphs.Add(graph);
            }
            else
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF will be saved without the graph.");
            }

            // Save the document – guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Unable to save PDF because GDI+ (libgdiplus) is missing on this platform.");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for DllNotFoundException.
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