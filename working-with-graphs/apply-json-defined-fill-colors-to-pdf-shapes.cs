using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ShapeConfig
{
    // Mapping from shape type (e.g., "Rectangle") to color name (e.g., "LightGray")
    public Dictionary<string, string> FillColors { get; set; } = new Dictionary<string, string>();
}

class Program
{
    static void Main()
    {
        const string configPath = "config.json";   // JSON file with fill color definitions
        const string outputPath = "output.pdf";

        // Load configuration
        ShapeConfig config = LoadConfig(configPath);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 200)
            Graph graph = new Graph(400, 200);

            // Example: add a rectangle if a fill color is defined for it
            if (config.FillColors.TryGetValue("Rectangle", out string rectColorName))
            {
                // Create a rectangle shape (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 100, 150, 80);
                // Set visual properties via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = GetColorByName(rectColorName),
                    Color = Aspose.Pdf.Color.Black,   // stroke color
                    LineWidth = 1
                };
                graph.Shapes.Add(rect);
            }

            // Example: add an ellipse if a fill color is defined for it
            if (config.FillColors.TryGetValue("Ellipse", out string ellipseColorName))
            {
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(250, 100, 150, 80);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = GetColorByName(ellipseColorName),
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };
                graph.Shapes.Add(ellipse);
            }

            // Example: add a line (lines do not have FillColor, only stroke)
            if (config.FillColors.TryGetValue("Line", out string lineColorName))
            {
                float[] linePoints = { 50, 250, 350, 250 };
                Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = GetColorByName(lineColorName),
                    LineWidth = 2
                };
                graph.Shapes.Add(line);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF (using the standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Loads the JSON configuration file into a ShapeConfig instance
    static ShapeConfig LoadConfig(string path)
    {
        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"Configuration file not found: {path}");
            return new ShapeConfig(); // return empty config
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<ShapeConfig>(json) ?? new ShapeConfig();
    }

    // Retrieves an Aspose.Pdf.Color instance by its static property name (e.g., "LightGray")
    static Aspose.Pdf.Color GetColorByName(string colorName)
    {
        // Try to find a static property on Aspose.Pdf.Color matching the name (case‑insensitive)
        PropertyInfo? prop = typeof(Aspose.Pdf.Color).GetProperty(
            colorName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

        if (prop != null && prop.PropertyType == typeof(Aspose.Pdf.Color))
        {
            return (Aspose.Pdf.Color)prop.GetValue(null)!;
        }

        // Fallback: use a default color if the name is not recognized
        Console.Error.WriteLine($"Color '{colorName}' not found. Using LightGray as default.");
        return Aspose.Pdf.Color.LightGray;
    }
}