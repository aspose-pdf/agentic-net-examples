using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ThemeConfig
{
    public string? Theme { get; set; }
    public OverlayColor? OverlayColor { get; set; }
}

class OverlayColor
{
    // Expected range: 0‑1 for each component
    public double R { get; set; }
    public double G { get; set; }
    public double B { get; set; }
    public double A { get; set; } // Opacity (0‑1)
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
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

        // Load theme configuration (JSON)
        ThemeConfig? config = JsonSerializer.Deserialize<ThemeConfig>(File.ReadAllText(configFilePath));
        if (config == null || config.OverlayColor == null)
        {
            Console.Error.WriteLine("Invalid or missing overlay configuration.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine page size
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a Graph container that covers the whole page
                Graph overlay = new Graph(pageWidth, pageHeight);

                // Create a rectangle shape that spans the entire page
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)pageWidth,
                    (float)pageHeight);

                // Convert 0‑1 color components to 0‑255 byte values
                int a = (int)Math.Round(config.OverlayColor.A * 255);
                int r = (int)Math.Round(config.OverlayColor.R * 255);
                int g = (int)Math.Round(config.OverlayColor.G * 255);
                int b = (int)Math.Round(config.OverlayColor.B * 255);

                // Set visual properties via GraphInfo (fill color with alpha)
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.FromArgb(a, r, g, b),
                    // No border needed for a pure overlay
                    Color = Color.Transparent,
                    LineWidth = 0f
                };

                // Add the rectangle to the graph
                overlay.Shapes.Add(rect);

                // Insert the graph as the first element on the page so it acts as an overlay
                page.Paragraphs.Insert(0, overlay);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}
