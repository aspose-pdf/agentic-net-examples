using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph (vector graphic container) – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);
            graph.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Black,   // border color
                LineWidth = 1.0f                  // border thickness
            };

            // Rectangle shape inside the graph (float parameters)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 300f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Line shape inside the graph (float array)
            var line = new Aspose.Pdf.Drawing.Line(new float[] { 50f, 150f, 350f, 250f });
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Add the graph (vector graphic) to the page
            page.Paragraphs.Add(graph);

            // ---------- Save the PDF with GDI+ guard ----------
            // On Windows the native GDI+ library is always present.
            // On macOS / Linux Aspose.Pdf may require libgdiplus; if it is missing, Document.Save throws a
            // TypeInitializationException that wraps a DllNotFoundException. We handle this gracefully.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was saved without rendering the vector graph.");
                    // Optionally, you could remove the graph before saving or provide a fallback.
                }
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
