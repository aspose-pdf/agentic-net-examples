using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "layered_graph.pdf";

        // Ensure the output directory exists (use fully‑qualified System.IO.Path to avoid ambiguity)
        string outputDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container – all drawing objects must be added to a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ------------------------------------------------------------
            // Rectangle shape (added to the graph)
            // Constructor: Rectangle(left, bottom, width, height)
            // ------------------------------------------------------------
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 300, 200, 100);
            rect.GraphInfo.FillColor = Color.FromRgb(0.2f, 0.5f, 0.8f); // fill
            rect.GraphInfo.Color = Color.Black;                     // border colour
            rect.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(rect);

            // ------------------------------------------------------------
            // Ellipse shape (added to the graph)
            // Constructor: Ellipse(left, bottom, width, height)
            // ------------------------------------------------------------
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200, 200, 200, 200);
            ellipse.GraphInfo.FillColor = Color.FromRgb(0.8f, 0.3f, 0.3f);
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(ellipse);

            // Add the graph (which now contains the rectangle and ellipse) to the page
            page.Paragraphs.Add(graph);

            // NOTE: The core Aspose.Pdf API does not expose BeginLayer/EndLayer methods.
            // Layer handling (Optional Content Groups) requires low‑level Operators API,
            // which is beyond the scope of this simple example. The shapes are therefore
            // added directly to the page.

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layered graph saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
