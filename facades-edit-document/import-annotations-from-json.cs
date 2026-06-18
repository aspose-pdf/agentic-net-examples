using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

// Define a model that matches the expected JSON structure
public class AnnotationDefinition
{
    public string Type { get; set; }          // e.g., "WebLink", "ApplicationLink", "FreeText"
    public int Page { get; set; }             // 1‑based page number
    public RectangleDto Rect { get; set; }    // rectangle coordinates
    public string Content { get; set; }       // text for FreeText or tooltip
    public string Target { get; set; }        // URL for WebLink or file path for ApplicationLink
    public string Color { get; set; }         // optional color name or hex (e.g., "Red" or "#FF0000")
}

// Helper DTO for rectangle values (all values are in points)
public class RectangleDto
{
    public int X { get; set; }      // lower‑left X
    public int Y { get; set; }      // lower‑left Y
    public int Width { get; set; }
    public int Height { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string jsonPath       = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load annotation definitions from JSON
        List<AnnotationDefinition> annotations;
        try
        {
            string json = File.ReadAllText(jsonPath);
            annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Use PdfContentEditor (facade) to apply annotations
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            foreach (var ann in annotations)
            {
                // Convert our DTO rectangle to System.Drawing.Rectangle (required by the facade)
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                    ann.Rect.X,
                    ann.Rect.Y,
                    ann.Rect.Width,
                    ann.Rect.Height);

                // Optional color parsing – default to Black if parsing fails
                System.Drawing.Color color = System.Drawing.Color.Black;
                if (!string.IsNullOrWhiteSpace(ann.Color))
                {
                    try
                    {
                        // Try known color name first
                        var known = System.Drawing.Color.FromName(ann.Color);
                        if (known.IsKnownColor) color = known;
                        else
                        {
                            // Try hex format #RRGGBB
                            if (ann.Color.StartsWith("#") && ann.Color.Length == 7)
                            {
                                int r = Convert.ToInt32(ann.Color.Substring(1, 2), 16);
                                int g = Convert.ToInt32(ann.Color.Substring(3, 2), 16);
                                int b = Convert.ToInt32(ann.Color.Substring(5, 2), 16);
                                color = System.Drawing.Color.FromArgb(r, g, b);
                            }
                        }
                    }
                    catch { /* ignore parsing errors, keep default */ }
                }

                // Apply annotation based on its type
                switch (ann.Type?.Trim().ToLowerInvariant())
                {
                    case "weblink":
                        // Create a web link annotation (URL)
                        // Overload without color parameter is sufficient; color can be added via the 4‑arg overload if needed
                        editor.CreateWebLink(rect, ann.Target ?? string.Empty, ann.Page);
                        break;

                    case "applicationlink":
                        // Create a link that launches an external application/file
                        editor.CreateApplicationLink(rect, ann.Target ?? string.Empty, ann.Page);
                        break;

                    case "freetext":
                        // Create a free‑text annotation with supplied content
                        // The 3‑arg overload creates the annotation on the specified page
                        editor.CreateFreeText(rect, ann.Content ?? string.Empty, ann.Page);
                        break;

                    default:
                        Console.WriteLine($"Unsupported annotation type: {ann.Type}");
                        break;
                }
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
    }
}