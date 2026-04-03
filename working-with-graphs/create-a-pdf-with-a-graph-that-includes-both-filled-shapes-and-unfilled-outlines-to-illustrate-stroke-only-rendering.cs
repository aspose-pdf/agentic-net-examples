using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Document lifecycle must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as the constructor now expects double
            Graph graph = new Graph(400.0, 300.0)
            {
                // Position the graph on the page (coordinates are from the bottom‑left corner)
                Left = 50,
                Top  = 400
            };

            // ---------- Filled shape (rectangle) ----------
            // Rectangle constructor: (left, bottom, width, height) – expects float values
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 100f);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // Fill the rectangle
                Color     = Aspose.Pdf.Color.Blue,      // Stroke color
                LineWidth = 2f                         // float literal
            };
            graph.Shapes.Add(filledRect);

            // ---------- Stroke‑only shape (ellipse) ----------
            // Ellipse constructor: (left, bottom, width, height) – expects float values
            var outlineEllipse = new Aspose.Pdf.Drawing.Ellipse(200f, 0f, 150f, 150f);
            outlineEllipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Transparent, // No fill – transparent background
                Color     = Aspose.Pdf.Color.Red,        // Stroke color
                LineWidth = 3f                         // float literal
            };
            graph.Shapes.Add(outlineEllipse);

            // Add the graph (with its shapes) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with filled and stroke‑only shapes saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. " +
                                  "Saving PDFs that contain Graph objects requires libgdiplus. " +
                                  "Install libgdiplus or run this code on Windows to generate the file.");
            }
        }
    }
}
