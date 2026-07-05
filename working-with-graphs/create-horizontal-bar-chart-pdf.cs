using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Bar‑chart parameters
            double barWidth   = 30;   // width of each bar
            double maxHeight  = 200;  // maximum bar height
            double startX     = 50;   // left margin
            double startY     = 100;  // baseline for bars
            double spacing    = 40;   // horizontal distance between bar starts

            // Add ten rectangles with incremental X coordinates
            for (int i = 0; i < 10; i++)
            {
                double left   = startX + i * spacing;
                double height = maxHeight * (i + 1) / 10.0; // example varying height

                // Create a rectangle shape (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)left,
                    (float)startY,
                    (float)barWidth,
                    (float)height);

                // Set visual appearance via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.2 + i * 0.08, 0.4, 0.6),
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(rect);
            }

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart saved to '{outputPath}'.");
    }
}