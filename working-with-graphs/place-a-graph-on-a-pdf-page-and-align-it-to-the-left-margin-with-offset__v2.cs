using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define graph size (width, height) in points
            double graphWidth = 300; // example width
            double graphHeight = 200; // example height

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has GDI+, so we can safely create and render the graph
                Graph graph = new Graph(graphWidth, graphHeight);
                graph.HorizontalAlignment = HorizontalAlignment.Left;
                graph.Left = 20; // offset from the left edge of the page
                graph.Title = new TextFragment("Sample Graph");
                page.Paragraphs.Add(graph);

                // Save the PDF
                doc.Save(outputPath);
                Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
            }
            else
            {
                // On non‑Windows platforms libgdiplus (GDI+) is usually missing.
                // Skip graph rendering and just save the document (or handle the error gracefully).
                Console.WriteLine("libgdiplus (GDI+) is not available on this platform; saving PDF without the graph.");
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (graph omitted).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Failed to save PDF because GDI+ is missing and the document contains drawing objects.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
