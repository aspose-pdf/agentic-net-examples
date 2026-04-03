using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based).
            Page page = pdfDoc.Pages.Add();

            // Use the Graph constructor that accepts double values (the old float overload is obsolete).
            Graph graph = new Graph(400.0, 200.0);

            // Rectangle expects float parameters – supply them explicitly.
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // use Aspose.Pdf.Color, not System.Drawing.Color
                Color = Aspose.Pdf.Color.Black,        // use Aspose.Pdf.Color, not System.Drawing.Color
                LineWidth = 1f // float literal as required by GraphInfo
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Guard the Save call – on non‑Windows platforms Aspose.Pdf needs libgdiplus (GDI+).
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Graph exported to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping PDF save because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}
