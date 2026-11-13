using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "layered_graphics.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph that covers the whole page
            Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

            // ---------- First shape (semi‑transparent red circle) ----------
            Ellipse redEllipse = new Ellipse(100, 300, 300, 500);
            // Use alpha channel (128 = 50 % opacity) for transparency
            redEllipse.GraphInfo.FillColor = Color.FromArgb(128, 255, 0, 0); // red fill
            redEllipse.GraphInfo.Color = Color.FromArgb(128, 255, 0, 0);     // red stroke (optional)
            graph.Shapes.Add(redEllipse);

            // ---------- Second shape (semi‑transparent blue circle) ----------
            Ellipse blueEllipse = new Ellipse(200, 300, 400, 500);
            blueEllipse.GraphInfo.FillColor = Color.FromArgb(128, 0, 0, 255); // blue fill
            blueEllipse.GraphInfo.Color = Color.FromArgb(128, 0, 0, 255);     // blue stroke (optional)
            graph.Shapes.Add(blueEllipse);

            // Add the graph (containing both ellipses) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF. Document.Save(string) always writes PDF regardless of extension.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layered graphics saved to '{outputPath}'.");
    }
}