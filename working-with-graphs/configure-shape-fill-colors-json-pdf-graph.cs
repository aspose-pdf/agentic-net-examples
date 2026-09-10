using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Helper to convert a color name (e.g., "LightGray") to Aspose.Pdf.Color
    static Color GetColor(string name)
    {
        // Try to get a static property with the given name from Aspose.Pdf.Color
        var prop = typeof(Color).GetProperty(name);
        if (prop != null && prop.PropertyType == typeof(Color))
        {
            var value = prop.GetValue(null);
            return value != null ? (Color)value : Color.Black;
        }
        // Fallback to Black if the name is not found
        return Color.Black;
    }

    static void Main()
    {
        const string configPath = "config.json";
        const string outputPath = "output.pdf";

        // Load configuration: shape type -> color name
        Dictionary<string, string> fillColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            fillColors = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
        else
        {
            // Default colors if config file is missing
            fillColors["Aspose.Pdf.Rectangle"] = "LightGray";
            fillColors["Ellipse"] = "Yellow";
            fillColors["Line"] = "Red";
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 200) – use double as recommended
            Graph graph = new Graph(400.0, 200.0);

            // ---------- Rectangle shape (Aspose.Pdf.Drawing.Rectangle) ----------
            // Constructor: left, bottom, width, height (float values)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = GetColor(fillColors.GetValueOrDefault("Aspose.Pdf.Rectangle", "LightGray")),
                Color = Color.Black,          // stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // ---------- Ellipse ----------
            var ellipseShape = new Ellipse(150f, 0f, 250f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = GetColor(fillColors.GetValueOrDefault("Ellipse", "Yellow")),
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // ---------- Line ----------
            float[] linePoints = { 0f, 150f, 300f, 150f };
            var lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = GetColor(fillColors.GetValueOrDefault("Line", "Red")),
                LineWidth = 2f
            };
            graph.Shapes.Add(lineShape);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
