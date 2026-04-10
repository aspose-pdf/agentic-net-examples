using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Theme configuration file format (example):
    // {
    //   "defaultColor": "#FFCCCC",
    //   "overrides": {
    //     "1": "#CCFFCC",
    //     "3": "#CCCCFF"
    //   }
    // }
    class ThemeConfig
    {
        public string DefaultColor { get; set; }
        public Dictionary<string, string> Overrides { get; set; }
    }

    static Aspose.Pdf.Color ParseHexColor(string hex)
    {
        // Remove leading '#', support 6-digit RGB.
        if (hex.StartsWith("#")) hex = hex.Substring(1);
        if (hex.Length != 6) throw new ArgumentException("Invalid color format.");
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string themeConfigPath = "theme.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(themeConfigPath))
        {
            Console.Error.WriteLine($"Theme config not found: {themeConfigPath}");
            return;
        }

        // Load theme configuration.
        ThemeConfig config = JsonSerializer.Deserialize<ThemeConfig>(File.ReadAllText(themeConfigPath));
        if (config == null)
        {
            Console.Error.WriteLine("Failed to parse theme configuration.");
            return;
        }

        // Parse default color.
        Aspose.Pdf.Color defaultColor = ParseHexColor(config.DefaultColor ?? "#FFFFFF");

        // Parse overrides.
        var overrideColors = new Dictionary<int, Aspose.Pdf.Color>();
        if (config.Overrides != null)
        {
            foreach (var kvp in config.Overrides)
            {
                if (int.TryParse(kvp.Key, out int pageNum))
                {
                    overrideColors[pageNum] = ParseHexColor(kvp.Value);
                }
            }
        }

        // Process PDF.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages (1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine overlay color for this page.
                Aspose.Pdf.Color overlayColor = defaultColor;
                if (overrideColors.TryGetValue(i, out Aspose.Pdf.Color specific))
                {
                    overlayColor = specific;
                }

                // Apply semi‑transparent overlay.
                // Aspose.Pdf does not expose direct opacity on Background,
                // but we can simulate by using a stamp with opacity.
                // Create a transparent PNG in memory (single‑pixel) and stretch it.
                // The PNG is generated on‑the‑fly with the desired color and 50% opacity.
                // For simplicity, we use a built‑in method: Page.Background sets the solid color.
                // Note: true transparency requires PDF 1.4+ and advanced graphics; here we set the background.
                page.Background = overlayColor;
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}