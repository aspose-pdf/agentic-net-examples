using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, Line, Rectangle, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const double offset = 20.0; // points offset from left margin

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Graph dimensions (use double as required by the non‑obsolete constructor)
            const double graphWidth = 400.0;
            const double graphHeight = 300.0;

            // Position the graph: left margin + offset, top margin + offset
            double left = page.PageInfo.Margin.Left + offset;
            double top = page.PageInfo.Margin.Top + offset;

            // Y origin in PDF is bottom‑left, so compute lower‑left Y coordinate
            double lly = page.PageInfo.Height - top - graphHeight;

            // Create the graph with double parameters (non‑obsolete constructor)
            Graph graph = new Graph(graphWidth, graphHeight);
            graph.Left = (float)left;
            graph.Top = (float)lly;

            // Sample data for a simple column chart
            double[] values = { 10, 20, 30 };
            double max = 30; // maximum value for scaling
            int barCount = values.Length;
            float spacing = 10f; // space between bars
            float barWidth = (float)((graphWidth - (spacing * (barCount + 1))) / barCount);
            float scale = (float)(graphHeight / max);

            // Draw X‑axis line
            Line xAxis = new Line(new float[] { 0, 0, (float)graphWidth, 0 });
            xAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);

            // Draw Y‑axis line
            Line yAxis = new Line(new float[] { 0, 0, 0, (float)graphHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(yAxis);

            // Draw bars
            for (int i = 0; i < barCount; i++)
            {
                float barHeight = (float)(values[i] * scale);
                float x = spacing + i * (barWidth + spacing);
                // Rectangle defined by lower‑left (x,0) and upper‑right (x+barWidth, barHeight)
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(x, 0, x + barWidth, barHeight);
                bar.GraphInfo.FillColor = Color.SteelBlue;
                bar.GraphInfo.LineWidth = 1f;
                graph.Shapes.Add(bar);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}
