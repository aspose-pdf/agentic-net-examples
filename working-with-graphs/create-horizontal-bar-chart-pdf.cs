using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "BarChart.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Fixed vertical position and size for all bars
            double barHeight = 20;
            double barY = 500; // distance from bottom of the page

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Loop to create ten rectangles with incremental X coordinates
            for (int i = 0; i < 10; i++)
            {
                // Incremental X coordinate (spacing of 30 points)
                double barX = 50 + i * 30;

                // Width of each bar (e.g., 20 points)
                double barWidth = 20;

                // Define the rectangle shape (lower‑left and upper‑right corners)
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)barX,
                    (float)barY,
                    (float)(barX + barWidth),
                    (float)(barY + barHeight));

                // Styling via GraphInfo – fill color, border color and line width
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = (i % 2 == 0) ? Color.LightBlue : Color.LightGreen,
                    Color = Color.Black,          // Border color
                    LineWidth = 0.5f               // Border thickness
                };

                // Add the rectangle to the graph container
                graph.Shapes.Add(rect);
            }

            // Add the graph (which now contains all rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
    }
}
