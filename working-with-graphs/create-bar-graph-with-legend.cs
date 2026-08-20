using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ReportGenerator
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define graph dimensions
            double graphWidth = 400;
            double graphHeight = 200;
            double graphLeft = 100;   // X position of the graph
            double graphBottom = 400; // Y position of the graph (used for legend calculations)

            // Create a Graph container (use double constructor as the float one is obsolete)
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = graphLeft
                // Bottom property is not required; positioning is handled via Left and the stored graphBottom variable
            };

            // Category data (example values)
            double[] values = { 120, 80, 150 };
            string[] categories = { "Category A", "Category B", "Category C" };
            Aspose.Pdf.Color[] colors = {
                Aspose.Pdf.Color.FromArgb(255, 99, 149, 237),   // Blue
                Aspose.Pdf.Color.FromArgb(255, 255, 192, 0),    // Orange
                Aspose.Pdf.Color.FromArgb(255, 112, 173, 71)    // Green
            };

            // Calculate rectangle width based on values (simple proportional scaling)
            double maxVal = 150; // maximum value for scaling
            float rectHeight = 30f;
            float startX = 0f;
            float startY = 0f;

            for (int i = 0; i < values.Length; i++)
            {
                float rectWidth = (float)((values[i] / maxVal) * graphWidth);

                // Create a rectangle shape (drawing.Rectangle) – constructor expects float arguments
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    startX,
                    startY,
                    rectWidth,
                    rectHeight);

                // Set visual properties via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = colors[i],
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };

                // Add rectangle to the graph
                graph.Shapes.Add(rect);

                // Move Y position for next bar
                startY += rectHeight + 10f;
            }

            // ---- Legend ----
            // Position for legend items (calculated using stored graphBottom)
            double legendX = graphLeft + graphWidth + 20;
            double legendY = graphBottom + graphHeight - 20;
            float legendBoxSize = 12f;
            float legendSpacing = 20f;

            for (int i = 0; i < categories.Length; i++)
            {
                // Small colored box – added to the same graph so it shares the coordinate system
                Aspose.Pdf.Drawing.Rectangle legendBox = new Aspose.Pdf.Drawing.Rectangle(
                    (float)legendX,
                    (float)(legendY - i * legendSpacing),
                    legendBoxSize,
                    legendBoxSize);

                legendBox.GraphInfo = new GraphInfo
                {
                    FillColor = colors[i],
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(legendBox);

                // Text label next to the box
                TextFragment label = new TextFragment(categories[i])
                {
                    // Position the text fragment
                    Position = new Position((float)legendX + legendBoxSize + 5, (float)(legendY - i * legendSpacing) + 2),
                    // Set text color for readability
                    TextState = { ForegroundColor = Aspose.Pdf.Color.Black, FontSize = 12 }
                };
                page.Paragraphs.Add(label);
            }

            // Add the graph (with bars and legend boxes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF report
            doc.Save("ReportWithGraph.pdf");
        }

        Console.WriteLine("PDF report generated: ReportWithGraph.pdf");
    }
}
