using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class PageSettingsConfig
{
    public double Width { get; set; }
    public double Height { get; set; }
    public string BackgroundColor { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string configJsonPath = "pageSettings.json";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configJsonPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configJsonPath}");
            return;
        }

        // Load configuration
        PageSettingsConfig config;
        try
        {
            string json = File.ReadAllText(configJsonPath);
            config = JsonSerializer.Deserialize<PageSettingsConfig>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Parse background color (hex string like "#RRGGBB" or "RRGGBB")
        Aspose.Pdf.Color bgColor = ParseHexColor(config.BackgroundColor);

        using (Document doc = new Document(inputPdfPath))
        {
            // Apply settings to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Adjust page size
                page.PageInfo.Width = config.Width;
                page.PageInfo.Height = config.Height;

                // Create a Graph container (requires double dimensions)
                Graph graph = new Graph(config.Width, config.Height);

                // Drawing rectangle expects float arguments
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)config.Width,
                    (float)config.Height);

                rect.GraphInfo = new GraphInfo
                {
                    FillColor = bgColor,
                    Color = bgColor,
                    LineWidth = 0f
                };

                // Add rectangle to the graph and the graph to the page
                graph.Shapes.Add(rect);
                page.Paragraphs.Add(graph);
            }

            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page settings applied and saved to '{outputPdfPath}'.");
    }

    private static Aspose.Pdf.Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            return Aspose.Pdf.Color.Transparent;
        }
        string clean = hex.TrimStart('#');
        if (clean.Length != 6)
        {
            return Aspose.Pdf.Color.Transparent;
        }
        int r = Convert.ToInt32(clean.Substring(0, 2), 16);
        int g = Convert.ToInt32(clean.Substring(2, 2), 16);
        int b = Convert.ToInt32(clean.Substring(4, 2), 16);
        return Aspose.Pdf.Color.FromArgb(255, r, g, b);
    }
}