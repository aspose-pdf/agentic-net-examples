using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a single page
            Page page = pdfDocument.Pages.Add();

            // Define size for each graph (width x height in points)
            double graphWidth = 250.0;
            double graphHeight = 200.0;

            // On non‑Windows platforms Aspose.Pdf.Drawing.Graph requires GDI+ (libgdiplus).
            // To avoid a TypeInitializationException we create the graphs only on Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Top‑left graph
                Graph graph1 = new Graph(graphWidth, graphHeight);
                graph1.Left = 50.0;
                graph1.Top = 750.0 - graphHeight; // A4 height approx 842 pt
                graph1.Title = new TextFragment("Graph 1");

                // Top‑right graph
                Graph graph2 = new Graph(graphWidth, graphHeight);
                graph2.Left = 350.0;
                graph2.Top = 750.0 - graphHeight;
                graph2.Title = new TextFragment("Graph 2");

                // Bottom‑left graph
                Graph graph3 = new Graph(graphWidth, graphHeight);
                graph3.Left = 50.0;
                graph3.Top = 500.0 - graphHeight;
                graph3.Title = new TextFragment("Graph 3");

                // Bottom‑right graph
                Graph graph4 = new Graph(graphWidth, graphHeight);
                graph4.Left = 350.0;
                graph4.Top = 500.0 - graphHeight;
                graph4.Title = new TextFragment("Graph 4");

                // Add the graphs to the page (Paragraphs collection)
                page.Paragraphs.Add(graph1);
                page.Paragraphs.Add(graph2);
                page.Paragraphs.Add(graph3);
                page.Paragraphs.Add(graph4);
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform – Graph objects require GDI+ (libgdiplus). Skipping graph creation.");
            }

            // Save the resulting PDF – guard the call for platforms without libgdiplus.
            try
            {
                pdfDocument.Save("combined-graphs.pdf");
                Console.WriteLine("PDF saved to 'combined-graphs.pdf'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                // Optionally, you could fallback to a different saving method or inform the user.
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
