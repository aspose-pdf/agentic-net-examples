using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_footer.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Configure page size (A4) and 1‑inch margins (72 points = 1 inch)
            PageInfo pageInfo = doc.PageInfo;
            pageInfo.Width = PageSize.A4.Width;
            pageInfo.Height = PageSize.A4.Height;
            // Use MarginInfo (not the non‑existent Margin class)
            pageInfo.Margin = new MarginInfo(72, 72, 72, 72); // left, top, right, bottom

            // Add a single page
            Page page = doc.Pages.Add();

            // Define chart dimensions: width = page width minus left/right margins,
            // height = 100 points, positioned just above the bottom margin (footer area)
            const double chartHeight = 100;
            double chartWidth = pageInfo.Width - pageInfo.Margin.Left - pageInfo.Margin.Right;
            double chartLeft = pageInfo.Margin.Left;
            double chartBottom = pageInfo.Margin.Bottom; // Y coordinate from bottom of page

            // Create a Graph (manual bar chart) with the calculated size
            Graph graph = new Graph(chartWidth, chartHeight)
            {
                Left = chartLeft,
                Top = chartBottom,
                Title = new TextFragment("Quarterly Sales (k$)")
            };

            // Draw axes
            // X‑axis
            Line xAxis = new Line(new float[] { 0, 0, (float)chartWidth, 0 });
            xAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);
            // Y‑axis
            Line yAxis = new Line(new float[] { 0, 0, 0, (float)chartHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(yAxis);

            // Data for the bar chart
            double[] values = { 30, 45, 25, 60 };
            string[] categories = { "Q1", "Q2", "Q3", "Q4" };
            double maxValue = 60; // could be calculated with values.Max()
            double scale = chartHeight / maxValue;

            // Simple layout: equal spacing, bar width = 1/5 of chart width
            int barCount = values.Length;
            double barSpacing = chartWidth / (barCount * 2.0);
            double barWidth = barSpacing; // make bar width equal to spacing for a tidy look

            for (int i = 0; i < barCount; i++)
            {
                double barHeight = values[i] * scale;
                double x = barSpacing + i * 2 * barSpacing; // start after first spacing

                // Rectangle shape for the bar (drawing rectangle, fully qualified)
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle((float)x, 0, (float)barWidth, (float)barHeight);
                bar.GraphInfo.FillColor = Color.Blue;
                bar.GraphInfo.Color = Color.Blue; // border colour (optional)
                graph.Shapes.Add(bar);

                // Add category label under each bar
                TextFragment label = new TextFragment(categories[i]);
                label.TextState.FontSize = 9;
                label.TextState.ForegroundColor = Color.Black;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                // Position the label – use a TextFragment with a Position relative to the page
                // Y coordinate is just below the X‑axis (bottom margin - 12 points)
                label.Position = new Position(chartLeft + x + barWidth / 2, chartBottom - 12);
                page.Paragraphs.Add(label);
            }

            // Add the graph to the page (it will be placed in the footer area)
            page.Paragraphs.Add(graph);

            // Optional footer text placed just below the chart within the bottom margin
            TextFragment footer = new TextFragment("Confidential – Company XYZ");
            footer.TextState.FontSize = 9;
            footer.TextState.ForegroundColor = Color.Gray;
            // Position: left margin, a few points below the chart
            footer.Position = new Position(pageInfo.Margin.Left, pageInfo.Margin.Bottom - 24);
            page.Paragraphs.Add(footer);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
