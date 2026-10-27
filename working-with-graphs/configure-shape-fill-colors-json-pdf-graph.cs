using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace GraphBuilder
{
    // Represents the configuration for default fill colors per shape type.
    public class FillColorConfig
    {
        // Key: shape name (e.g., "Bar", "Line", "Pie")
        // Value: color in hex format "#RRGGBB"
        public Dictionary<string, string> ShapeColors { get; set; } = new Dictionary<string, string>();
    }

    class Program
    {
        // Parses a hex color string (e.g., "#FFAA33") into an Aspose.Pdf.Color.
        static Aspose.Pdf.Color ParseHexColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Aspose.Pdf.Color.Transparent; // fallback

            // Remove leading '#', if present.
            hex = hex.TrimStart('#');

            // Ensure we have exactly 6 characters.
            if (hex.Length != 6)
                throw new ArgumentException($"Invalid hex color: {hex}");

            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            return Aspose.Pdf.Color.FromRgb(r, g, b);
        }

        static void Main()
        {
            const string configPath = "config.json";
            const string outputPath = "output.pdf";

            // Load configuration file.
            FillColorConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<FillColorConfig>(json) ?? new FillColorConfig();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load config: {ex.Message}");
                // Use empty config as fallback.
                config = new FillColorConfig();
            }

            // Determine default fill color for bars; fallback to light gray if not defined.
            Aspose.Pdf.Color barFillColor = config.ShapeColors.TryGetValue("Bar", out string barHex)
                ? ParseHexColor(barHex)
                : Aspose.Pdf.Color.LightGray;

            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a page.
                Page page = doc.Pages.Add();

                // Simple bar chart data.
                double[] values = { 120, 80, 150, 60, 200 };
                double maxVal = 0;
                foreach (double v in values) if (v > maxVal) maxVal = v;

                // Layout parameters.
                double chartWidth = 400;
                double chartHeight = 300;
                double chartX = 50;               // left side of chart
                double chartY = 500;              // top side of chart (Y grows upwards in PDF coordinates)

                double barWidth = chartWidth / values.Length * 0.7;
                double gap = (chartWidth - (barWidth * values.Length)) / (values.Length + 1);

                // Create a Graph container that will hold all drawing shapes.
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // ----- Draw axes -----
                // X‑axis line
                Line xAxis = new Line(new float[] { (float)chartX, (float)chartY, (float)(chartX + chartWidth), (float)chartY });
                xAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                xAxis.GraphInfo.LineWidth = 0.5f;
                graph.Shapes.Add(xAxis);

                // Y‑axis line
                Line yAxis = new Line(new float[] { (float)chartX, (float)chartY, (float)chartX, (float)(chartY - chartHeight) });
                yAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                yAxis.GraphInfo.LineWidth = 0.5f;
                graph.Shapes.Add(yAxis);

                // ----- Draw each bar using the configured fill color -----
                for (int i = 0; i < values.Length; i++)
                {
                    double barHeight = (values[i] / maxVal) * chartHeight;
                    double left = chartX + gap + i * (barWidth + gap);
                    double bottom = chartY - barHeight;

                    // Rectangle constructor: left, bottom, width, height
                    Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle((float)left, (float)bottom, (float)barWidth, (float)barHeight);
                    bar.GraphInfo.FillColor = barFillColor;
                    bar.GraphInfo.Color = Aspose.Pdf.Color.Black; // border color
                    bar.GraphInfo.LineWidth = 0.5f;

                    graph.Shapes.Add(bar);
                }

                // Add the graph (containing axes and bars) to the page.
                page.Paragraphs.Add(graph);

                // Save the PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
        }
    }
}
