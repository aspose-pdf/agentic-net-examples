using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

public class PageAdjustment
{
    // 1‑based page number
    public int PageNumber { get; set; }
    // Rotation in degrees (0, 90, 180, 270)
    public int? Rotation { get; set; }
    // Width and height in points (1 point = 1/72 inch)
    public double? Width { get; set; }
    public double? Height { get; set; }
    // Background color as hex "#RRGGBB" or known name (e.g., "LightGray")
    public string? BackgroundColor { get; set; }
}

public class PdfJob
{
    public string InputPath { get; set; } = null!;
    public string OutputPath { get; set; } = null!;
    public List<PageAdjustment> Adjustments { get; set; } = null!;
}

public class ConfigRoot
{
    public List<PdfJob> Jobs { get; set; } = null!;
}

class Program
{
    static void Main(string[] args)
    {
        string configFile = args.Length > 0 ? args[0] : "config.json";

        if (!File.Exists(configFile))
        {
            Console.Error.WriteLine($"Config file not found: {configFile}");
            return;
        }

        string json = File.ReadAllText(configFile);
        ConfigRoot? config = JsonSerializer.Deserialize<ConfigRoot>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (config?.Jobs == null || config.Jobs.Count == 0)
        {
            Console.Error.WriteLine("No jobs defined in the configuration.");
            return;
        }

        foreach (var job in config.Jobs)
        {
            if (!File.Exists(job.InputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {job.InputPath}");
                continue;
            }

            // Load the PDF document once – we will apply all adjustments (rotation, size, background) here.
            using (Document doc = new Document(job.InputPath))
            {
                foreach (var adj in job.Adjustments)
                {
                    // Validate page range (Aspose.Pdf uses 1‑based indexing)
                    if (adj.PageNumber < 1 || adj.PageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {adj.PageNumber} out of range in {job.InputPath}");
                        continue;
                    }

                    Page page = doc.Pages[adj.PageNumber];

                    // ----- Rotation -----
                    if (adj.Rotation.HasValue)
                    {
                        page.Rotate = adj.Rotation.Value switch
                        {
                            0 => Rotation.None,
                            90 => Rotation.on90,
                            180 => Rotation.on180,
                            270 => Rotation.on270,
                            _ => Rotation.None
                        };
                    }

                    // ----- Size -----
                    if (adj.Width.HasValue && adj.Height.HasValue)
                    {
                        page.PageInfo.Width = adj.Width.Value;
                        page.PageInfo.Height = adj.Height.Value;
                    }

                    // ----- Background color -----
                    if (!string.IsNullOrEmpty(adj.BackgroundColor))
                    {
                        // Create a Graph container that covers the whole page.
                        Graph graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

                        // Rectangle constructor expects (left, bottom, width, height) as floats.
                        var rect = new Aspose.Pdf.Drawing.Rectangle(
                            0f,
                            0f,
                            (float)page.PageInfo.Width,
                            (float)page.PageInfo.Height)
                        {
                            // Styling is done via GraphInfo, not a direct FillColor property.
                            GraphInfo = new GraphInfo
                            {
                                FillColor = ParseColor(adj.BackgroundColor),
                                // Optional: no border line.
                                Color = Aspose.Pdf.Color.Transparent
                            }
                        };

                        // Add the rectangle shape to the graph and then the graph to the page.
                        graph.Shapes.Add(rect);
                        page.Paragraphs.Add(graph);
                    }
                }

                // Save final output PDF
                doc.Save(job.OutputPath);
            }

            Console.WriteLine($"Processed: {job.OutputPath}");
        }
    }

    // Helper to convert a color string to Aspose.Pdf.Color
    static Aspose.Pdf.Color ParseColor(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Aspose.Pdf.Color.Transparent;

        value = value.Trim();

        // Hex format "#RRGGBB"
        if (value.StartsWith("#") && value.Length == 7)
        {
            int r = Convert.ToInt32(value.Substring(1, 2), 16);
            int g = Convert.ToInt32(value.Substring(3, 2), 16);
            int b = Convert.ToInt32(value.Substring(5, 2), 16);
            return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
        }

        // Simple named colors (case‑insensitive)
        return value.ToLower() switch
        {
            "lightgray" => Aspose.Pdf.Color.LightGray,
            "white" => Aspose.Pdf.Color.White,
            "black" => Aspose.Pdf.Color.Black,
            _ => Aspose.Pdf.Color.Transparent
        };
    }
}
