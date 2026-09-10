using System;
using System.IO;
using System.Text.Json;
using System.Drawing; // System.Drawing.Rectangle and System.Drawing.Color are required by PdfContentEditor overloads
using Aspose.Pdf.Facades;

// POCO that matches the JSON structure for annotation definitions
public class AnnotationDefinition
{
    public string? Type { get; set; }          // e.g., "Text", "WebLink"
    public double X1 { get; set; }            // lower‑left X
    public double Y1 { get; set; }            // lower‑left Y
    public double X2 { get; set; }            // upper‑right X
    public double Y2 { get; set; }            // upper‑right Y
    public string? Contents { get; set; }      // annotation text or URL
    public string? ColorHex { get; set; }      // optional, e.g., "#FF0000"
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonPath = "annotations.json";
        const string outputPdfPath = "output.pdf";

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

        // Read and deserialize the JSON file containing annotation definitions
        AnnotationDefinition[]? annotations;
        try
        {
            string json = File.ReadAllText(jsonPath);
            annotations = JsonSerializer.Deserialize<AnnotationDefinition[]>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read/parse JSON: {ex.Message}");
            return;
        }

        if (annotations == null || annotations.Length == 0)
        {
            Console.Error.WriteLine("No annotation definitions were found in the JSON file.");
            return;
        }

        // Initialize the PdfContentEditor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Process each annotation definition and create the corresponding annotation
        foreach (var def in annotations)
        {
            // Build the rectangle for the annotation (PdfContentEditor expects System.Drawing.Rectangle)
            var sysRect = new System.Drawing.Rectangle(
                (int)def.X1,
                (int)def.Y1,
                (int)(def.X2 - def.X1),
                (int)(def.Y2 - def.Y1));

            // Determine the color (default to Black if not supplied or parsing fails)
            System.Drawing.Color sysColor = System.Drawing.Color.Black;
            if (!string.IsNullOrWhiteSpace(def.ColorHex))
            {
                try
                {
                    sysColor = ParseHexColor(def.ColorHex);
                }
                catch
                {
                    // ignore parsing errors and keep default black
                }
            }

            // Create the annotation based on its type
            switch (def.Type?.Trim().ToLowerInvariant())
            {
                case "text":
                    // Create a free‑text annotation. The overload expects System.Drawing.Rectangle.
                    editor.CreateText(
                        sysRect,
                        def.Contents ?? string.Empty,
                        Guid.NewGuid().ToString(),
                        true,               // open flag
                        string.Empty,       // subject (optional)
                        1);                 // page number (1‑based)
                    break;

                case "weblink":
                    // Create a web‑link annotation. This overload also expects System.Drawing.Rectangle.
                    editor.CreateWebLink(sysRect, def.Contents ?? string.Empty, 1);
                    break;

                case "line":
                    // Create a line annotation. Color must be System.Drawing.Color.
                    editor.CreateLine(
                        sysRect,
                        string.Empty,
                        0, 0, 0, 0, 0, 0,
                        sysColor,
                        string.Empty,
                        null,
                        null);
                    break;

                case "squarecircle":
                    // Create a square‑circle (highlight) annotation.
                    // The overload requires a border width as the last argument.
                    editor.CreateSquareCircle(sysRect, string.Empty, sysColor, true, 1, 1);
                    break;

                default:
                    Console.WriteLine($"Unsupported annotation type: {def.Type}");
                    break;
            }
        }

        // Save the modified PDF
        editor.Save(outputPdfPath);
        Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
    }

    // Helper: converts a hex color string (e.g., "#RRGGBB") to System.Drawing.Color
    private static System.Drawing.Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Invalid color string.");

        // Remove leading '#', if present
        hex = hex.TrimStart('#');

        if (hex.Length != 6)
            throw new ArgumentException("Hex color must be 6 characters.");

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return System.Drawing.Color.FromArgb(r, g, b);
    }
}
