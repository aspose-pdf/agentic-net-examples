using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace GraphBuilderWithConfig
{
    // Represents the JSON configuration for default fill colors.
    // Example config.json:
    // {
    //   "FillColors": {
    //     "Rectangle": "LightGray",
    //     "Ellipse": "Yellow",
    //     "Line": "Red"
    //   }
    // }
    public class ShapeConfig
    {
        public Dictionary<string, string> FillColors { get; set; } = new Dictionary<string, string>();
    }

    class Program
    {
        // Retrieves an Aspose.Pdf.Color instance from its static property name.
        static Aspose.Pdf.Color GetColorByName(string colorName)
        {
            // Fallback to Black if the name is not found.
            var colorProp = typeof(Aspose.Pdf.Color).GetProperty(colorName);
            if (colorProp != null && colorProp.PropertyType == typeof(Aspose.Pdf.Color))
            {
                return (Aspose.Pdf.Color)colorProp.GetValue(null);
            }
            return Aspose.Pdf.Color.Black;
        }

        static void Main()
        {
            const string configPath = "config.json";
            const string outputPath = "shapes_graph.pdf";

            // Load configuration.
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            ShapeConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ShapeConfig>(json) ?? new ShapeConfig();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse configuration: {ex.Message}");
                return;
            }

            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a single page.
                Page page = doc.Pages.Add();

                // Create a Graph container (width: 400, height: 300).
                Graph graph = new Graph(400, 300);

                // ---------- Rectangle ----------
                // Define rectangle dimensions.
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 200, 150, 100);
                // Apply fill color from config (default to LightGray if missing).
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = GetColorByName(
                        config.FillColors.TryGetValue("Rectangle", out var rectColor) ? rectColor : "LightGray")
                };
                graph.Shapes.Add(rectShape);

                // ---------- Ellipse ----------
                Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250, 200, 150, 100);
                ellipseShape.GraphInfo = new GraphInfo
                {
                    FillColor = GetColorByName(
                        config.FillColors.TryGetValue("Ellipse", out var ellipseColor) ? ellipseColor : "Yellow")
                };
                graph.Shapes.Add(ellipseShape);

                // ---------- Line ----------
                // Line constructor expects a float array: {x1, y1, x2, y2}
                float[] linePoints = { 50f, 50f, 350f, 50f };
                Aspose.Pdf.Drawing.Line lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
                lineShape.GraphInfo = new GraphInfo
                {
                    // Lines do not use FillColor; we set the stroke color via Color.
                    Color = GetColorByName(
                        config.FillColors.TryGetValue("Line", out var lineColor) ? lineColor : "Red"),
                    LineWidth = 2
                };
                graph.Shapes.Add(lineShape);

                // Add the graph to the page.
                page.Paragraphs.Add(graph);

                // Save the document (PDF format).
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with configured shapes saved to '{outputPath}'.");
        }
    }
}