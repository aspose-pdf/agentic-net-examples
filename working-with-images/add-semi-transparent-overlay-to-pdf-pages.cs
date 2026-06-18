using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Simple class to deserialize theme configuration
    private class ThemeConfig
    {
        public required string OverlayColor { get; set; }   // Hex string, e.g. "#FFAA00"
        public double Opacity { get; set; } = 1.0;          // 0.0 – 1.0 (used for alpha channel)
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_overlay.pdf";
        const string themeConfigPath = "theme.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(themeConfigPath))
        {
            Console.Error.WriteLine($"Theme configuration not found: {themeConfigPath}");
            return;
        }

        // Load theme configuration
        ThemeConfig config;
        try
        {
            string json = File.ReadAllText(themeConfigPath);
            config = JsonSerializer.Deserialize<ThemeConfig>(json) ?? throw new InvalidDataException("Deserialized config is null.");
            if (string.IsNullOrWhiteSpace(config.OverlayColor))
                throw new InvalidDataException("OverlayColor is missing in theme configuration.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read theme configuration: {ex.Message}");
            return;
        }

        // Convert hex color to Aspose.Pdf.Color (including opacity)
        Aspose.Pdf.Color overlayColor;
        try
        {
            overlayColor = ParseHexToColor(config.OverlayColor, config.Opacity);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid color value in configuration: {ex.Message}");
            return;
        }

        // Load the PDF, apply overlay, and save
        using (Document doc = new Document(inputPdfPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.Background = overlayColor;
            }

            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Parses a hex color string ("#RRGGBB" or "RRGGBB") and applies the supplied opacity (0‑1) to create an Aspose.Pdf.Color.
    /// </summary>
    private static Aspose.Pdf.Color ParseHexToColor(string hex, double opacity)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string cannot be null or empty.");

        // Remove leading '#'
        hex = hex.TrimStart('#');

        if (hex.Length != 6 && hex.Length != 8)
            throw new ArgumentException("Hex color must be 6 (RRGGBB) or 8 (AARRGGBB) characters long.");

        // If 8 characters, the first two are alpha – we will combine with opacity parameter.
        int a = 255;
        int startIdx = 0;
        if (hex.Length == 8)
        {
            a = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            startIdx = 2;
        }

        int r = int.Parse(hex.Substring(startIdx, 2), NumberStyles.HexNumber);
        int g = int.Parse(hex.Substring(startIdx + 2, 2), NumberStyles.HexNumber);
        int b = int.Parse(hex.Substring(startIdx + 4, 2), NumberStyles.HexNumber);

        // Apply opacity (0‑1) to alpha channel
        a = (int)Math.Round(a * Math.Clamp(opacity, 0.0, 1.0));

        return Aspose.Pdf.Color.FromArgb(a, r, g, b);
    }
}
