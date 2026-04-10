using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document (lifecycle handled by using)
        using (Document doc = new Document())
        {
            // Add a blank page to host the chart
            Page page = doc.Pages.Add();

            // Define the size of the graph area
            double graphWidth = 400;
            double graphHeight = 300;
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page (left, top from page bottom)
                Left = 50,
                Top = 400
            };

            // Sample data for the bar chart
            double[] values = { 120, 80, 150, 60 };
            string[] categories = { "Q1", "Q2", "Q3", "Q4" };
            double maxValue = 200;          // Maximum value for scaling
            double barWidth = 40;           // Width of each bar
            double spacing = 20;            // Space between bars

            // Draw Y‑axis line
            float[] yLine = { 0, 0, 0, (float)graphHeight };
            Line yAxis = new Line(yLine)
            {
                GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Black, LineWidth = 1 }
            };
            graph.Shapes.Add(yAxis);

            // Draw X‑axis line
            float[] xLine = { 0, 0, (float)graphWidth, 0 };
            Line xAxis = new Line(xLine)
            {
                GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Black, LineWidth = 1 }
            };
            graph.Shapes.Add(xAxis);

            // Draw each bar with a custom fill color
            for (int i = 0; i < values.Length; i++)
            {
                double barHeight = (values[i] / maxValue) * graphHeight;
                double left = spacing + i * (barWidth + spacing);

                // Use Aspose.Pdf.Drawing.Rectangle for a shape inside a Graph
                var bar = new Aspose.Pdf.Drawing.Rectangle(
                    (float)left,
                    0f,
                    (float)barWidth,
                    (float)barHeight);

                // Alternate colors for visual distinction
                Aspose.Pdf.Color fill = (i % 2 == 0) ? Aspose.Pdf.Color.Chocolate : Aspose.Pdf.Color.Chartreuse;
                bar.GraphInfo = new GraphInfo
                {
                    FillColor = fill,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(bar);

                // Category label below the bar – add to the page, offset by graph position
                TextFragment tf = new TextFragment(categories[i])
                {
                    Position = new Position(
                        (float)(graph.Left + left + barWidth / 2 - 10),
                        (float)(graph.Top - 20))
                };
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(tf);
            }

            // Y‑axis tick marks and numeric labels (0, 50, 100, 150, 200)
            for (int i = 0; i <= 4; i++)
            {
                double y = i * (graphHeight / 4);

                // Tick mark
                float[] tick = { -5, (float)y, 0, (float)y };
                Line tickLine = new Line(tick)
                {
                    GraphInfo = new GraphInfo { Color = Aspose.Pdf.Color.Black, LineWidth = 1 }
                };
                graph.Shapes.Add(tickLine);

                // Numeric label – add to the page, offset by graph position
                TextFragment label = new TextFragment((i * 50).ToString())
                {
                    Position = new Position(
                        (float)(graph.Left - 30),
                        (float)(graph.Top - y - 5))
                };
                label.TextState.FontSize = 12;
                label.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(label);
            }

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "BarChart.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
