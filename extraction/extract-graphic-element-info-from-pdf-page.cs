using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // for GraphicsAbsorber and GraphicElement

class GraphicInfo
{
    public List<string> Operators { get; set; } = new();
    public RectangleDto Rectangle { get; set; } = new();
    public PointDto Position { get; set; } = new();
}

class RectangleDto
{
    public double LLX { get; set; }
    public double LLY { get; set; }
    public double URX { get; set; }
    public double URY { get; set; }
}

class PointDto
{
    public double X { get; set; }
    public double Y { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJson = "graphic_info.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; retrieve page 5
            Page page = doc.Pages[5];

            // Use GraphicsAbsorber to obtain vector graphic elements on the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            if (absorber.Elements.Count == 0)
            {
                Console.Error.WriteLine("No GraphicElement found on page 5.");
                return;
            }

            // Take the first graphic element
            GraphicElement graphic = absorber.Elements[0];

            // Extract operators as strings (Operator objects implement ToString())
            var ops = new List<string>();
            foreach (var op in graphic.Operators)
                ops.Add(op.ToString());

            // Extract rectangle and position (fully qualified types)
            Aspose.Pdf.Rectangle rect = graphic.Rectangle;
            Aspose.Pdf.Point pos = graphic.Position;

            // Prepare DTO for JSON serialization
            GraphicInfo info = new GraphicInfo
            {
                Operators = ops,
                Rectangle = new RectangleDto
                {
                    LLX = rect.LLX,
                    LLY = rect.LLY,
                    URX = rect.URX,
                    URY = rect.URY
                },
                Position = new PointDto
                {
                    X = pos.X,
                    Y = pos.Y
                }
            };

            // Serialize to indented JSON
            string json = JsonSerializer.Serialize(info, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Graphic element information saved to '{outputJson}'.");
        }
    }
}
