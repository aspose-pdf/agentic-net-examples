using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Simple POCO to deserialize the JSON configuration file.
    // The keys are shape names, the values are hex color strings (e.g., "#FF0000").
    private class ShapeColorConfig
    {
        public Dictionary<string, string> Colors { get; set; } = new Dictionary<string, string>();
    }

    // Convert a hex color string (e.g., "#RRGGBB") to an Aspose.Pdf.Color instance.
    private static Aspose.Pdf.Color HexToAsposeColor(string hex)
    {
        // Remove any leading '#'.
        if (hex.StartsWith("#")) hex = hex.Substring(1);
        if (hex.Length != 6) throw new ArgumentException("Hex color must be 6 characters.");

        // Parse the RGB components.
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);

        // Aspose.Pdf.Color expects values in the range 0.0 – 1.0.
        return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
    }

    static void Main()
    {
        const string configPath = "config.json";
        const string outputPdf = "graph.pdf";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and parse the configuration file.
        ShapeColorConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ShapeColorConfig>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new ShapeColorConfig();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Resolve colors for known shapes, falling back to a default if not defined.
        Aspose.Pdf.Color rectFill = config.Colors.TryGetValue("Rectangle", out var rectHex)
            ? HexToAsposeColor(rectHex)
            : Aspose.Pdf.Color.LightGray; // default

        Aspose.Pdf.Color circleFill = config.Colors.TryGetValue("Circle", out var circleHex)
            ? HexToAsposeColor(circleHex)
            : Aspose.Pdf.Color.LightGray; // default

        // Create the PDF document and add a page.
        using (Document pdf = new Document())
        {
            Page page = pdf.Pages.Add();

            // Create a Graph container – this is the only paragraph type that can hold shapes.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Rectangle: left=50, bottom=500, width=200, height=100 (as described in the comment).
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 500, 200, 100);
            rect.GraphInfo.FillColor = rectFill;               // Fill colour
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;    // Border colour
            rect.GraphInfo.LineWidth = 1;                      // Border thickness
            graph.Shapes.Add(rect);

            // Ellipse (circle) centred at (300,550) with radius 50 → left=250, bottom=500, width=100, height=100.
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(250, 500, 100, 100);
            ellipse.GraphInfo.FillColor = circleFill;
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.Black;
            ellipse.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(ellipse);

            // Add the graph (which now contains the shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}
