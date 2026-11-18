using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_footer.pdf";

        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set page margins (MarginInfo is the correct type)
            page.PageInfo.Margin = new MarginInfo
            {
                Top = 72,    // 1 inch
                Bottom = 72, // 1 inch
                Left = 72,
                Right = 72
            };

            // Footer‑area graph dimensions
            double chartHeight = 40; // points
            double chartWidth = page.PageInfo.Width - page.PageInfo.Margin.Left - page.PageInfo.Margin.Right;

            // Create a Graph object (no Chart API in Aspose.Pdf)
            Graph graph = new Graph(chartWidth, chartHeight)
            {
                // Position the graph inside the bottom margin
                Left = (float)page.PageInfo.Margin.Left,
                Top = (float)page.PageInfo.Margin.Bottom, // top aligns with bottom‑margin line
                Width = (float)chartWidth,
                Height = (float)chartHeight
            };

            // Sample data for a simple column chart
            double[] values = { 120, 150, 180, 200 };
            double max = 200; // maximum value for scaling
            int barCount = values.Length;
            float barSpacing = 5f; // space between bars
            float availableWidth = (float)chartWidth - (barCount + 1) * barSpacing;
            float barWidth = availableWidth / barCount;

            // Draw X‑axis
            Line xAxis = new Line(new float[] { 0, 0, (float)chartWidth, 0 });
            xAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);

            // Draw Y‑axis
            Line yAxis = new Line(new float[] { 0, 0, 0, (float)chartHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(yAxis);

            // Draw bars
            for (int i = 0; i < barCount; i++)
            {
                float barHeight = (float)(values[i] / max) * (float)chartHeight;
                float x = barSpacing + i * (barWidth + barSpacing);
                // Use fully‑qualified Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(x, 0, x + barWidth, barHeight);
                bar.GraphInfo.FillColor = Color.Blue;
                bar.GraphInfo.Color = Color.Blue; // border colour (optional)
                graph.Shapes.Add(bar);
            }

            // Optional title – placed above the graph using a TextFragment inside a Paragraph
            TextFragment title = new TextFragment("Quarterly Sales")
            {
                TextState = { Font = FontRepository.FindFont("Helvetica"), FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center
            };
            // Add a small vertical space before the graph
            page.Paragraphs.Add(new TextFragment("\n"));
            page.Paragraphs.Add(title);
            page.Paragraphs.Add(new TextFragment("\n"));
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph in footer saved to '{outputPath}'.");
    }
}
