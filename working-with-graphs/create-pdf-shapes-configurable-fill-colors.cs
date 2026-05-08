using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Simple configuration model: shape name -> color name
    class ShapeColorConfig
    {
        public Dictionary<string, string> FillColors { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    static void Main()
    {
        const string configPath = "config.json";
        const string outputPath = "shapes_graph.pdf";

        // Load configuration (expects JSON like: {"FillColors":{"Rectangle":"LightGray","Ellipse":"Yellow","Line":"Red"}})
        ShapeColorConfig config = LoadConfig(configPath);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500 points, height: 300 points) – use double literals as required by the non‑obsolete constructor
            Graph graph = new Graph(500.0, 300.0);

            // ----- Rectangle -----
            // Aspose.Pdf.Drawing.Rectangle constructor: (left, bottom, width, height) – expects float values
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = GetColor(config.FillColors.GetValueOrDefault("Rectangle", "LightGray")),
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse -----
            // Aspose.Pdf.Drawing.Ellipse constructor: (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(300f, 150f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = GetColor(config.FillColors.GetValueOrDefault("Ellipse", "Yellow")),
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipse);

            // ----- Line -----
            // Aspose.Pdf.Drawing.Line constructor takes a float array {x1, y1, x2, y2}
            float[] linePoints = { 100f, 50f, 400f, 50f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = GetColor(config.FillColors.GetValueOrDefault("Line", "Red")),
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with shapes saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }

    // Loads the JSON configuration file into a ShapeColorConfig instance
    static ShapeColorConfig LoadConfig(string path)
    {
        if (!File.Exists(path))
        {
            // If the config file is missing, use defaults
            return new ShapeColorConfig();
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<ShapeColorConfig>(json) ?? new ShapeColorConfig();
    }

    // Maps a color name (e.g., "Red") to the corresponding Aspose.Pdf.Color static property
    static Aspose.Pdf.Color GetColor(string name)
    {
        // Fallback to Transparent if the name is not recognized
        if (string.IsNullOrWhiteSpace(name))
            return Aspose.Pdf.Color.Transparent;

        // Use reflection to find the static property on Aspose.Pdf.Color
        var prop = typeof(Aspose.Pdf.Color).GetProperty(name,
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
        if (prop != null && prop.PropertyType == typeof(Aspose.Pdf.Color))
        {
            return (Aspose.Pdf.Color)prop.GetValue(null)!;
        }

        // Default color if lookup fails
        return Aspose.Pdf.Color.Transparent;
    }
}
