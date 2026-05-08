using System;
using System.Runtime.InteropServices;
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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the size of the graph (width x height)
            double graphWidth = 400;
            double graphHeight = 300;

            // Create the graph and position it on the page
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = 50,   // distance from the left edge of the page
                Top  = 400   // distance from the bottom edge of the page
            };

            // ----- Draw axes -----
            // Y‑axis
            Line yAxis = new Line(new float[] { 0, 0, 0, (float)graphHeight });
            yAxis.GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Black, LineWidth = 1 };
            graph.Shapes.Add(yAxis);

            // X‑axis
            Line xAxis = new Line(new float[] { 0, (float)graphHeight, (float)graphWidth, (float)graphHeight });
            xAxis.GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Black, LineWidth = 1 };
            graph.Shapes.Add(xAxis);

            // ----- Sample data for the bar chart -----
            double[] values = { 120, 80, 150, 60, 200 };
            string[] categories = { "A", "B", "C", "D", "E" };

            // Determine scaling factor so the tallest bar fits the graph height
            double maxVal = 0;
            foreach (double v in values) if (v > maxVal) maxVal = v;
            double scale = graphHeight / maxVal;

            // Bar dimensions
            double barWidth = graphWidth / values.Length * 0.6;
            double gap = (graphWidth - barWidth * values.Length) / (values.Length + 1);

            // Custom colors for each bar
            Aspose.Pdf.Color[] barColors = {
                Aspose.Pdf.Color.Chocolate,
                Aspose.Pdf.Color.Chartreuse,
                Aspose.Pdf.Color.LightBlue,
                Aspose.Pdf.Color.LightCoral,
                Aspose.Pdf.Color.LightGreen
            };

            // ----- Draw bars and X‑axis labels -----
            for (int i = 0; i < values.Length; i++)
            {
                double barHeight = values[i] * scale;
                double x = gap + i * (barWidth + gap);

                // Create a rectangle representing the bar (use Drawing.Rectangle, not Pdf.Rectangle)
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,          // lower‑left X
                    0f,                // lower‑left Y (graph bottom)
                    (float)barWidth,   // width
                    (float)barHeight   // height
                );
                bar.GraphInfo = new GraphInfo
                {
                    FillColor = barColors[i % barColors.Length],
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(bar);

                // Add category label below the X‑axis (positioned on the page, not inside the graph)
                TextFragment label = new TextFragment(categories[i])
                {
                    Position = new Position(x + barWidth / 2, graphHeight + 10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                label.TextState.FontSize = 12;
                page.Paragraphs.Add(label);
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // ----- Save the PDF document -----
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus (GDI+). Guard the save call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bar chart saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus (GDI+) is required for PDF creation on this platform. " +
                                  "The PDF was generated in memory but not saved to disk.");
            }
        }
    }
}
