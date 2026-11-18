using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ReportGenerator
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "Report.pdf";

        // Ensure the output directory exists (fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path)
        string outputDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block (lifecycle rule)
        using (Document pdf = new Document())
        {
            // Add a single page (pages are 1‑based)
            pdf.Pages.Add();
            Page page = pdf.Pages[1];

            // Define graph data: category name, value, and color
            var data = new (string Name, double Value, Color Fill)[]
            {
                ("Category A", 30, Color.FromRgb(0.8, 0.2, 0.2)), // Reddish
                ("Category B", 50, Color.FromRgb(0.2, 0.8, 0.2)), // Greenish
                ("Category C", 20, Color.FromRgb(0.2, 0.2, 0.8))  // Blueish
            };

            // Graph layout parameters
            double graphX = 100;      // left margin of graph area
            double graphY = 500;      // top of graph area
            double barWidth = 50;
            double barSpacing = 30;
            double maxBarHeight = 300; // maximum height for the highest value

            // Determine the maximum value to scale bars proportionally
            double maxValue = 0;
            foreach (var item in data)
                if (item.Value > maxValue) maxValue = item.Value;

            // Create a Graph container that will hold all shape objects
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Draw bars (colored rectangles) inside the graph
            for (int i = 0; i < data.Length; i++)
            {
                double barHeight = (data[i].Value / maxValue) * maxBarHeight;
                double left = graphX + i * (barWidth + barSpacing);
                double bottom = graphY - barHeight;

                // Rectangle constructor: (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle((float)left, (float)bottom, (float)barWidth, (float)barHeight);
                bar.GraphInfo.FillColor = data[i].Fill;          // Fill colour
                bar.GraphInfo.Color = Color.Black;               // Border colour
                bar.GraphInfo.LineWidth = 0.5f;                  // Border thickness
                graph.Shapes.Add(bar);

                // Add category label below each bar (TextFragment is a paragraph, so add directly to page)
                TextFragment label = new TextFragment(data[i].Name)
                {
                    TextState = { FontSize = 12, ForegroundColor = Color.Black }
                };
                // Position the label centred under the bar
                double labelX = left + (barWidth / 2);
                label.Position = new Position((float)labelX, (float)(graphY + 15));
                label.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(label);
            }

            // Legend layout parameters
            double legendX = graphX + data.Length * (barWidth + barSpacing) + 50;
            double legendY = graphY;
            double legendBoxSize = 12;
            double legendSpacing = 20;

            // Draw legend entries (boxes + text)
            for (int i = 0; i < data.Length; i++)
            {
                double boxLeft = legendX;
                double boxBottom = legendY - i * legendSpacing - legendBoxSize;

                Aspose.Pdf.Drawing.Rectangle legendBox = new Aspose.Pdf.Drawing.Rectangle((float)boxLeft, (float)boxBottom, (float)legendBoxSize, (float)legendBoxSize);
                legendBox.GraphInfo.FillColor = data[i].Fill;
                legendBox.GraphInfo.Color = Color.Black;
                legendBox.GraphInfo.LineWidth = 0.5f;
                graph.Shapes.Add(legendBox);

                // Text label next to the square
                TextFragment legendLabel = new TextFragment(data[i].Name)
                {
                    TextState = { FontSize = 12, ForegroundColor = Color.Black }
                };
                legendLabel.Position = new Position((float)(boxLeft + legendBoxSize + 5), (float)boxBottom);
                page.Paragraphs.Add(legendLabel);
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF (PDF format, no special SaveOptions needed)
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF report generated at '{outputPath}'.");
    }
}
