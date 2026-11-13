using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // ---------------------------------------------------------------------
            // 1. Define chart dimensions and scaling
            // ---------------------------------------------------------------------
            // Graph size (width x height)
            const double graphWidth = 500.0;   // use double as Graph ctor expects double
            const double graphHeight = 400.0;
            // Margins inside the graph for axes and labels
            const float leftMargin = 70f;   // space for Y‑axis labels
            const float bottomMargin = 70f; // space for X‑axis labels
            const float topMargin = 50f;    // space for title
            const float rightMargin = 30f;

            // Actual drawing area (where bars will be placed)
            float plotX = leftMargin;
            float plotY = bottomMargin;
            float plotWidth = (float)graphWidth - leftMargin - rightMargin;
            float plotHeight = (float)graphHeight - topMargin - bottomMargin;

            // Data for the bar chart
            string[] quarters = { "Q1", "Q2", "Q3", "Q4" };
            double[] values = { 15000, 23000, 18000, 20000 };
            // Custom colours for each bar (RGB values 0‑1)
            Color[] barColors =
            {
                Color.FromRgb(0.2, 0.6, 0.8), // light blue
                Color.FromRgb(0.8, 0.3, 0.2), // coral
                Color.FromRgb(0.4, 0.8, 0.4), // light green
                Color.FromRgb(0.9, 0.7, 0.2)  // gold
            };

            // Determine scaling factor (pixels per unit)
            double maxValue = 0;
            foreach (double v in values) if (v > maxValue) maxValue = v;
            // Add a small headroom so the tallest bar is not touching the top axis
            maxValue *= 1.1;
            double scale = plotHeight / maxValue;

            // ---------------------------------------------------------------------
            // 2. Create the Graph object (acts as a paragraph)
            // ---------------------------------------------------------------------
            Graph graph = new Graph(graphWidth, graphHeight); // uses double ctor (no longer obsolete)
            // Optional border for the whole graph area (visual aid)
            graph.Border = new BorderInfo(BorderSide.All, Color.Gray);
            // Title
            graph.Title = new TextFragment("Quarterly Sales");
            graph.Title.TextState.FontSize = 16;
            graph.Title.TextState.Font = FontRepository.FindFont("Helvetica");
            graph.Title.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            graph.Title.Margin = new MarginInfo { Top = 5, Bottom = 5 };

            // ---------------------------------------------------------------------
            // 3. Draw axes
            // ---------------------------------------------------------------------
            // Y‑axis line
            Line yAxis = new Line(new float[] { plotX, plotY, plotX, plotY + plotHeight });
            yAxis.GraphInfo.Color = Color.Black;
            yAxis.GraphInfo.LineWidth = 1f;
            graph.Shapes.Add(yAxis);

            // X‑axis line
            Line xAxis = new Line(new float[] { plotX, plotY, plotX + plotWidth, plotY });
            xAxis.GraphInfo.Color = Color.Black;
            xAxis.GraphInfo.LineWidth = 1f;
            graph.Shapes.Add(xAxis);

            // ---------------------------------------------------------------------
            // 4. Draw bars
            // ---------------------------------------------------------------------
            const float barWidth = 60f;
            const float barGap = 30f;
            for (int i = 0; i < values.Length; i++)
            {
                double val = values[i];
                float barHeight = (float)(val * scale);
                float left = plotX + i * (barWidth + barGap) + barGap / 2;
                float bottom = plotY;
                float right = left + barWidth;
                float top = bottom + barHeight;

                // Rectangle representing the bar
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(left, bottom, right, top);
                bar.GraphInfo.FillColor = barColors[i];
                bar.GraphInfo.LineWidth = 0.5f; // thin border
                bar.GraphInfo.Color = Color.Black; // border colour
                graph.Shapes.Add(bar);

                // X‑axis label under the bar
                TextFragment xLabel = new TextFragment(quarters[i]);
                xLabel.TextState.Font = FontRepository.FindFont("Helvetica");
                xLabel.TextState.FontSize = 12;
                // Position the label – centre it under the bar
                float labelX = left + barWidth / 2;
                xLabel.Position = new Position(labelX, plotY - 20);
                xLabel.TextState.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(xLabel);
            }

            // ---------------------------------------------------------------------
            // 5. Y‑axis numeric labels (optional – simple 5‑step grid)
            // ---------------------------------------------------------------------
            int steps = 5;
            for (int s = 0; s <= steps; s++)
            {
                double stepValue = maxValue * s / steps;
                float y = plotY + (float)(stepValue * scale);
                // Small tick mark
                Line tick = new Line(new float[] { plotX - 5, y, plotX, y });
                tick.GraphInfo.Color = Color.Black;
                tick.GraphInfo.LineWidth = 0.5f;
                graph.Shapes.Add(tick);

                // Label
                TextFragment yLabel = new TextFragment(((int)stepValue).ToString());
                yLabel.TextState.Font = FontRepository.FindFont("Helvetica");
                yLabel.TextState.FontSize = 10;
                yLabel.Position = new Position(plotX - 10, y - 5);
                yLabel.TextState.HorizontalAlignment = HorizontalAlignment.Right;
                page.Paragraphs.Add(yLabel);
            }

            // ---------------------------------------------------------------------
            // 6. Add the graph to the page
            // ---------------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart saved to '{outputPath}'.");
    }
}
