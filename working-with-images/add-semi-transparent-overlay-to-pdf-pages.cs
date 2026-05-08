using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ThemeConfig
{
    public required string Color { get; set; }      // Hex string, e.g. "#FFAA00"
    public required double Opacity { get; set; }    // 0.0 to 1.0
}

class Program
{
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

        // Load theme configuration
        ThemeConfig theme = JsonSerializer.Deserialize<ThemeConfig>(File.ReadAllText(themeConfigPath))!;
        if (theme == null || string.IsNullOrWhiteSpace(theme.Color))
        {
            Console.Error.WriteLine("Invalid theme configuration.");
            return;
        }

        // Parse hex color to Aspose.Pdf.Color (RGB) and also retrieve the raw R,G,B bytes
        var (baseColor, r, g, b) = ParseHexColor(theme.Color);

        // Ensure opacity is within 0..1 and compute alpha byte
        double opacity = Math.Max(0.0, Math.Min(1.0, theme.Opacity));
        int alpha = (int)Math.Round(opacity * 255);
        // Create a color that includes the alpha channel for transparency
        Aspose.Pdf.Color overlayColor = Aspose.Pdf.Color.FromArgb(alpha, r, g, b);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Apply the overlay to each page of the source document
            foreach (Page page in srcDoc.Pages)
            {
                // Create a Graph that covers the whole page
                Graph graph = new Graph(page.MediaBox.Width, page.MediaBox.Height);

                // Create a rectangle shape that fills the page
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.MediaBox.Width,
                    (float)page.MediaBox.Height);

                rect.GraphInfo = new GraphInfo
                {
                    FillColor = overlayColor,
                    Color = overlayColor // outline (optional)
                };

                graph.Shapes.Add(rect);

                // Insert the graph as the first paragraph so it appears behind existing content
                page.Paragraphs.Insert(0, graph);
            }

            // Save the modified PDF
            srcDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }

    // Helper: converts "#RRGGBB" or "RRGGBB" to Aspose.Pdf.Color (RGB, no alpha)
    // Returns the Aspose.Pdf.Color together with the integer R,G,B components (0‑255)
    static (Aspose.Pdf.Color color, int r, int g, int b) ParseHexColor(string hex)
    {
        string clean = hex.TrimStart('#');
        if (clean.Length != 6)
            throw new ArgumentException("Color must be in RRGGBB format.");

        int r = Convert.ToInt32(clean.Substring(0, 2), 16);
        int g = Convert.ToInt32(clean.Substring(2, 2), 16);
        int b = Convert.ToInt32(clean.Substring(4, 2), 16);
        // Aspose.Pdf.Color.FromRgb expects values in the range 0‑1
        Aspose.Pdf.Color aspColor = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
        return (aspColor, r, g, b);
    }
}
