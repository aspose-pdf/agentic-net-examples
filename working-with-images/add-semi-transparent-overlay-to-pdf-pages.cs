using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ThemeConfig
{
    public string Theme { get; set; } = null!; // non‑nullable, initialized to suppress warning
    public OverlayColorConfig OverlayColor { get; set; } = null!; // non‑nullable, initialized to suppress warning
}

class OverlayColorConfig
{
    public double R { get; set; }   // 0.0 – 1.0
    public double G { get; set; }
    public double B { get; set; }
    public double A { get; set; }   // 0.0 – 1.0 (opacity)
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
        ThemeConfig config;
        try
        {
            string json = File.ReadAllText(themeConfigPath);
            config = JsonSerializer.Deserialize<ThemeConfig>(json) ?? throw new InvalidDataException("Invalid theme config JSON.");
            if (config.OverlayColor == null)
                throw new InvalidDataException("OverlayColor section missing.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read theme config: {ex.Message}");
            return;
        }

        // Convert double (0‑1) to byte (0‑255) for Aspose.Pdf.Color
        int a = (int)Math.Round(config.OverlayColor.A * 255);
        int r = (int)Math.Round(config.OverlayColor.R * 255);
        int g = (int)Math.Round(config.OverlayColor.G * 255);
        int b = (int)Math.Round(config.OverlayColor.B * 255);
        Aspose.Pdf.Color overlayColor = Aspose.Pdf.Color.FromArgb(a, r, g, b);

        // Open PDF and add semi‑transparent overlay to each page
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a Graph that spans the whole page (float parameters required)
                Graph graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

                // Rectangle shape covering the entire page (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                    0f,                     // left
                    0f,                     // bottom
                    (float)page.PageInfo.Width,   // width
                    (float)page.PageInfo.Height); // height

                // Apply the semi‑transparent fill color
                shape.GraphInfo.FillColor = overlayColor;
                // Optional: remove border line
                shape.GraphInfo.Color = overlayColor; // same color, line width defaults to 0
                shape.GraphInfo.LineWidth = 0;

                // Add shape to graph and graph to page
                graph.Shapes.Add(shape);
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}
