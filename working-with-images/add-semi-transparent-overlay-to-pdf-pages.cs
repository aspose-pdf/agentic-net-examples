using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ThemeConfig
{
    public string OverlayColor { get; set; } = "#000000"; // default black
    public double Opacity { get; set; } = 0.5;           // default 50% opacity
}

class Program
{
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
            Console.Error.WriteLine($"Theme config not found: {themeConfigPath}");
            return;
        }

        // Load theme configuration
        ThemeConfig config;
        try
        {
            string json = File.ReadAllText(themeConfigPath);
            config = JsonSerializer.Deserialize<ThemeConfig>(json) ?? new ThemeConfig();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read theme config: {ex.Message}");
            return;
        }

        // Convert hex color to Aspose.Pdf.Color (values 0‑1)
        Aspose.Pdf.Color overlayColor = ParseHexColor(config.OverlayColor);

        // Ensure opacity is within 0‑1 range
        double opacity = Math.Max(0.0, Math.Min(1.0, config.Opacity));

        // Process PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine page size
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a rectangle that covers the whole page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);

                // Use SquareAnnotation (the correct class) as a full‑page overlay
                var overlay = new SquareAnnotation(page, rect)
                {
                    // Fill colour (interior) – the same as the overlay colour
                    InteriorColor = overlayColor,
                    // No border
                    Color = Aspose.Pdf.Color.Transparent,
                    Opacity = opacity
                };
                // Border must be created with the owning annotation instance
                overlay.Border = new Border(overlay) { Width = 0 };

                // Add annotation to the page
                page.Annotations.Add(overlay);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }

    // Helper: parse "#RRGGBB" or "RRGGBB" into Aspose.Pdf.Color
    static Aspose.Pdf.Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return Aspose.Pdf.Color.Black;

        hex = hex.TrimStart('#');

        if (hex.Length != 6)
            return Aspose.Pdf.Color.Black; // fallback

        try
        {
            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            // FromRgb expects values in the range 0‑1
            return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
        }
        catch
        {
            return Aspose.Pdf.Color.Black; // fallback on parse error
        }
    }
}
