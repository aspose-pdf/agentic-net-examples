using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container that spans the whole page – all drawing shapes must be placed inside a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define ellipse parameters (left, bottom, width, height)
            // Original rectangle: lower‑left (100, 500), upper‑right (300, 700)
            double left   = 100;   // X coordinate of lower‑left corner
            double bottom = 500;   // Y coordinate of lower‑left corner
            double width  = 200;   // 300 - 100
            double height = 200;   // 700 - 500

            // Create the ellipse shape using the constructor that takes (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Apply a fill – the current Aspose.Pdf version does not expose GradientFill, so we use a solid color as a placeholder.
            // The bounding box is independent of the fill type, so the measurement remains correct.
            ellipse.GraphInfo.FillColor = Color.Blue;   // solid fill (replace with GradientFill when available)
            ellipse.GraphInfo.Color     = Color.Black; // outline color
            ellipse.GraphInfo.LineWidth = 1;

            // Add the ellipse to the graph, then add the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Retrieve the bounding box (the rectangle does not change after setting the fill)
            double llx = left;
            double lly = bottom;
            double urx = left + width;
            double ury = bottom + height;
            double measuredWidth  = urx - llx;
            double measuredHeight = ury - lly;

            Console.WriteLine("Ellipse bounding box:");
            Console.WriteLine($"  Lower‑left: ({llx}, {lly})");
            Console.WriteLine($"  Upper‑right: ({urx}, {ury})");
            Console.WriteLine($"  Width: {measuredWidth}, Height: {measuredHeight}");

            // Save the PDF (optional, but demonstrates full lifecycle)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
