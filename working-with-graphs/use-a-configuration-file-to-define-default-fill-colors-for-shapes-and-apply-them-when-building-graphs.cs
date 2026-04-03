using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    // Simple configuration model: maps shape names to RGB color strings (e.g., "255,0,0")
    private class FillColorConfig
    {
        public string Rectangle { get; set; }
        public string Ellipse { get; set; }
        public string Line { get; set; }
    }

    static Aspose.Pdf.Color ParseColor(string rgb)
    {
        // Expect format "R,G,B" where each component is 0-255
        var parts = rgb.Split(',');
        if (parts.Length != 3) throw new FormatException($"Invalid color format: {rgb}");
        int r = int.Parse(parts[0]);
        int g = int.Parse(parts[1]);
        int b = int.Parse(parts[2]);
        return Aspose.Pdf.Color.FromRgb(r, g, b);
    }

    static void Main()
    {
        const string configPath = "fillcolors.json";   // JSON file defining default fill colors
        const string outputPath = "graph_output.pdf";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration
        FillColorConfig config = JsonSerializer.Deserialize<FillColorConfig>(File.ReadAllText(configPath));

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a graph that covers the whole page
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight);

            // ---------- Rectangle ----------
            // Position: (50, 600), size: 200x100
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 600f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = ParseColor(config.Rectangle),   // Fill color from config
                Color = Aspose.Pdf.Color.Black,            // Border color
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // ---------- Ellipse ----------
            // Position: (300, 600), size: 150x100
            Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(300f, 600f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = ParseColor(config.Ellipse),
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(ellipseShape);

            // ---------- Line ----------
            // Line from (100,400) to (400,400)
            float[] linePoints = { 100f, 400f, 400f, 400f };
            Aspose.Pdf.Drawing.Line lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = ParseColor(config.Line),
                LineWidth = 2
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}