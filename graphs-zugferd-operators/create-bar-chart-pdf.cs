using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class BarChartExample
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "BarChart.pdf";

        // Sample data for the bar chart
        double[] values = { 50, 120, 80, 150 };
        // Custom colors for each bar
        Color[] barColors = { Color.Chocolate, Color.Chartreuse, Color.Blue, Color.Red };

        // Dimensions for the graph (width x height in points)
        const double graphWidth = 500;
        const double graphHeight = 300;

        // Margins inside the graph area for axes
        const double leftMargin = 50;
        const double bottomMargin = 50;
        const double topMargin = 20;
        const double rightMargin = 20;

        // Calculate scaling factor for bar heights
        double maxValue = 0;
        foreach (double v in values) if (v > maxValue) maxValue = v;
        double yScale = (graphHeight - topMargin - bottomMargin) / maxValue;

        // Bar dimensions
        int barCount = values.Length;
        double barSpacing = 20;
        double barWidth = (graphWidth - leftMargin - rightMargin - (barCount - 1) * barSpacing) / barCount;

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (use double constructor as required by the API)
            Graph graph = new Graph(graphWidth, graphHeight);
            // Position the graph on the page using MarginInfo (left, bottom, right, top)
            graph.Margin = new MarginInfo(50, 400, 0, 0);

            // Draw X and Y axes using Line shapes
            // Y axis
            Line yAxis = new Line(new float[] { (float)leftMargin, (float)bottomMargin, (float)leftMargin, (float)(graphHeight - topMargin) });
            yAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
            graph.Shapes.Add(yAxis);

            // X axis
            Line xAxis = new Line(new float[] { (float)leftMargin, (float)bottomMargin, (float)(graphWidth - rightMargin), (float)bottomMargin });
            xAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
            graph.Shapes.Add(xAxis);

            // Add bars (use Aspose.Pdf.Drawing.Rectangle)
            for (int i = 0; i < barCount; i++)
            {
                double barHeight = values[i] * yScale;
                double x = leftMargin + i * (barWidth + barSpacing);
                double y = bottomMargin;

                var bar = new Aspose.Pdf.Drawing.Rectangle((float)x, (float)y, (float)barWidth, (float)barHeight);
                bar.GraphInfo = new GraphInfo
                {
                    FillColor = barColors[i % barColors.Length],
                    Color = Color.Black,
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(bar);
            }

            // Add the graph to the page before adding labels (labels are separate TextFragments)
            page.Paragraphs.Add(graph);

            // Add X‑axis labels (simple numeric labels)
            for (int i = 0; i < barCount; i++)
            {
                double x = leftMargin + i * (barWidth + barSpacing) + barWidth / 2;
                double y = bottomMargin - 15; // place below the X axis

                TextFragment label = new TextFragment($"Item {i + 1}");
                label.Position = new Position((float)x, (float)y);
                label.TextState.FontSize = 12;
                label.TextState.ForegroundColor = Color.Black;
                // Center the text horizontally
                label.TextState.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(label);
            }

            // Add Y‑axis labels (e.g., 0, max/2, max)
            for (int i = 0; i <= 2; i++)
            {
                double value = maxValue * i / 2;
                double y = bottomMargin + value * yScale;
                double x = leftMargin - 10; // left of Y axis

                TextFragment yLabel = new TextFragment($"{value}");
                yLabel.Position = new Position((float)x, (float)y);
                yLabel.TextState.FontSize = 12;
                yLabel.TextState.ForegroundColor = Color.Black;
                // Right‑align the label
                yLabel.TextState.HorizontalAlignment = HorizontalAlignment.Right;
                page.Paragraphs.Add(yLabel);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
    }
}
