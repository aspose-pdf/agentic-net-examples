using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "bar_chart.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold the rectangle shapes
            // Width and height are in points; adjust as needed for the page size
            Graph graph = new Graph(500, 200);
            graph.Left = 50;   // position from the left edge of the page
            graph.Top  = 600;  // position from the bottom edge of the page

            // Bar chart parameters
            double barWidth   = 30;   // width of each bar
            double spacing    = 10;   // space between bars
            double maxHeight  = 150;  // maximum bar height (for scaling)

            // Loop to create ten bars with incremental X positions
            for (int i = 0; i < 10; i++)
            {
                // X coordinate for the current bar
                double left = i * (barWidth + spacing);

                // Example height: increase with index (you could use real data here)
                double height = (i + 1) * (maxHeight / 10);

                // Create a rectangle shape (Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)left,          // left position within the graph
                    0,                    // bottom position (baseline)
                    (float)barWidth,      // width of the bar
                    (float)height);       // height of the bar

                // Set visual styling via GraphInfo (FillColor, border Color, LineWidth)
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.2 + i * 0.08, 0.4, 0.6), // varying fill color
                    Color     = Aspose.Pdf.Color.Black,                           // border color
                    LineWidth = 1
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);
            }

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart saved to '{outputPath}'.");
    }
}