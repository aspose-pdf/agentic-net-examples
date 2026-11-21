using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias to avoid ambiguity with Aspose.Pdf.Rectangle (annotation bounds)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_shadow.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – Graph is the only container that can hold drawing shapes.
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Rectangle dimensions (left, bottom, width, height)
            float left   = 100f;
            float bottom = 300f; // Bottom coordinate measured from the bottom of the page
            float width  = 200f;
            float height = 200f;

            // ------------------------------------------------------------
            // 1️⃣ Simulate a shadow by drawing an offset, semi‑transparent rectangle first.
            // ------------------------------------------------------------
            var shadowRect = new DrawRect(left + 5f, bottom - 5f, width, height);
            // Semi‑transparent gray (alpha 80 out of 255)
            shadowRect.GraphInfo.FillColor = Color.FromArgb(80, 128, 128, 128);
            // No border for the shadow shape
            shadowRect.GraphInfo.Color = Color.Transparent;
            graph.Shapes.Add(shadowRect);

            // ------------------------------------------------------------
            // 2️⃣ Main filled rectangle.
            // ------------------------------------------------------------
            var rect = new DrawRect(left, bottom, width, height);
            rect.GraphInfo.FillColor = Color.LightBlue;   // Fill color
            rect.GraphInfo.Color = Color.Black;           // Stroke (border) color
            rect.GraphInfo.LineWidth = 1f;                // Border thickness
            graph.Shapes.Add(rect);

            // Add the graph (which now contains the shadow + rectangle) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
