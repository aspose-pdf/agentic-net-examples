using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Ensure output directory exists
        string outputDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – all drawing shapes go into this Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // -------------------------------------------------
            // Filled rectangle with a black stroke (outline)
            // -------------------------------------------------
            // Rectangle constructor: (left, bottom, width, height)
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 150);
            filledRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue;   // Fill color
            filledRect.GraphInfo.Color = Aspose.Pdf.Color.Black;          // Stroke color
            filledRect.GraphInfo.LineWidth = 2;                            // Stroke width
            graph.Shapes.Add(filledRect);

            // -------------------------------------------------
            // Unfilled ellipse (stroke‑only rendering)
            // -------------------------------------------------
            // Ellipse constructor: (left, bottom, width, height)
            var outlineEllipse = new Aspose.Pdf.Drawing.Ellipse(350, 500, 200, 150);
            outlineEllipse.GraphInfo.Color = Aspose.Pdf.Color.Red;        // Stroke color
            outlineEllipse.GraphInfo.LineWidth = 2;                        // Stroke width
            // No FillColor set => transparent (stroke‑only)
            graph.Shapes.Add(outlineEllipse);

            // -------------------------------------------------
            // Filled polygon (triangle) as a data series
            // -------------------------------------------------
            var filledPolygon = new Aspose.Pdf.Drawing.Path(new Shape[]
            {
                new Line(new float[] { 100, 400, 200, 300 }),
                new Line(new float[] { 200, 300, 300, 400 }),
                new Line(new float[] { 300, 400, 100, 400 })
            });
            filledPolygon.GraphInfo.FillColor = Aspose.Pdf.Color.LightGreen;
            filledPolygon.GraphInfo.Color = Aspose.Pdf.Color.DarkGreen;
            filledPolygon.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(filledPolygon);

            // -------------------------------------------------
            // Stroke‑only polyline (outline of the same triangle)
            // -------------------------------------------------
            var outlinePolygon = new Aspose.Pdf.Drawing.Path(new Shape[]
            {
                new Line(new float[] { 350, 400, 450, 300 }),
                new Line(new float[] { 450, 300, 550, 400 }),
                new Line(new float[] { 550, 400, 350, 400 })
            });
            outlinePolygon.GraphInfo.Color = Aspose.Pdf.Color.DarkRed;
            outlinePolygon.GraphInfo.LineWidth = 1;
            // No FillColor => stroke‑only (transparent fill)
            graph.Shapes.Add(outlinePolygon);

            // Add the Graph (which now contains all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF (the Save method without options always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled and stroke‑only shapes saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
