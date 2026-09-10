using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Simple configuration model
    private class ThemeConfig
    {
        // Provide default values to satisfy non‑nullable warnings
        public string OverlayColor { get; set; } = "#FFFFFF"; // default white
        public double Opacity { get; set; } = 1.0;            // fully opaque
    }

    static Aspose.Pdf.Color ParseColor(string hex, double opacity)
    {
        // Remove leading '#', support 6‑digit hex
        if (hex.StartsWith("#")) hex = hex.Substring(1);
        if (hex.Length != 6) throw new ArgumentException("OverlayColor must be a 6‑digit hex value.");

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        // Aspose.Pdf.Color supports ARGB via FromArgb (alpha 0‑255)
        int a = (int)Math.Round(opacity * 255);
        return Aspose.Pdf.Color.FromArgb(a, r, g, b);
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string configFilePath = "themeConfig.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Load theme configuration
        ThemeConfig config;
        try
        {
            string json = File.ReadAllText(configFilePath);
            config = JsonSerializer.Deserialize<ThemeConfig>(json);
            if (config == null || string.IsNullOrWhiteSpace(config.OverlayColor))
                throw new InvalidDataException("Invalid configuration content.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Parse the overlay color with the requested opacity
        Aspose.Pdf.Color overlayColor;
        try
        {
            overlayColor = ParseColor(config.OverlayColor, config.Opacity);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid color/opacity: {ex.Message}");
            return;
        }

        // Process PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions from MediaBox
                double pageWidth  = page.MediaBox.URX - page.MediaBox.LLX;
                double pageHeight = page.MediaBox.URY - page.MediaBox.LLY;

                // Graph that covers the whole page (use float values as required by Graph ctor)
                Graph graph = new Graph((float)pageWidth, (float)pageHeight);

                // Drawing rectangle (Aspose.Pdf.Drawing.Rectangle) – the shape added to the graph
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)pageWidth, (float)pageHeight)
                {
                    GraphInfo = new GraphInfo
                    {
                        FillColor = overlayColor,   // semi‑transparent fill
                        Color     = overlayColor,   // stroke color (optional)
                        LineWidth = 0f
                    }
                };

                graph.Shapes.Add(rect);

                // Add the graph to the page's content
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}
