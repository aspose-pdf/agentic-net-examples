using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace BatchAnnotationDemo
{
    // Represents a single annotation definition read from JSON.
    public class AnnotationDefinition
    {
        public string Type { get; set; }          // e.g., "Text", "Link", "Square", "Circle", "Highlight"
        public int Page { get; set; }             // 1‑based page number
        public double Llx { get; set; }           // lower‑left X
        public double Lly { get; set; }           // lower‑left Y
        public double Urx { get; set; }           // upper‑right X
        public double Ury { get; set; }           // upper‑right Y
        public string Contents { get; set; }      // annotation text (if applicable)
        public string Color { get; set; }         // simple color name (e.g., "Yellow") or hex "#RRGGBB"
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string jsonDefPath  = "annotations.json";
            const string outputPdfPath = "output_annotated.pdf";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(jsonDefPath))
            {
                Console.Error.WriteLine($"JSON definition file not found: {jsonDefPath}");
                return;
            }

            // Load annotation definitions from JSON.
            List<AnnotationDefinition> definitions;
            try
            {
                string json = File.ReadAllText(jsonDefPath);
                definitions = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json);
                if (definitions == null) throw new Exception("Deserialized list is null.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read JSON definitions: {ex.Message}");
                return;
            }

            // Open the PDF document inside a using block (lifecycle rule).
            using (Document doc = new Document(inputPdfPath))
            {
                foreach (var def in definitions)
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing).
                    if (def.Page < 1 || def.Page > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {def.Page} for annotation type {def.Type}.");
                        continue;
                    }

                    // Build the rectangle (fully qualified to avoid ambiguity).
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(def.Llx, def.Lly, def.Urx, def.Ury);

                    // Resolve color (fallback to Yellow if parsing fails).
                    Aspose.Pdf.Color color = ResolveColor(def.Color) ?? Aspose.Pdf.Color.Yellow;

                    // Retrieve the target page.
                    Page page = doc.Pages[def.Page];

                    // Create and add the appropriate annotation based on the type string.
                    switch (def.Type?.Trim().ToLowerInvariant())
                    {
                        case "text":
                            {
                                var textAnn = new TextAnnotation(page, rect)
                                {
                                    Title = "Note",
                                    Contents = def.Contents ?? string.Empty,
                                    Color = color,
                                    Open = true,
                                    Icon = TextIcon.Note
                                };
                                page.Annotations.Add(textAnn);
                                break;
                            }
                        case "link":
                            {
                                // For a simple external link, use GoToURIAction.
                                var linkAnn = new LinkAnnotation(page, rect)
                                {
                                    Color = color
                                };
                                linkAnn.Action = new GoToURIAction(def.Contents ?? "https://example.com");
                                page.Annotations.Add(linkAnn);
                                break;
                            }
                        case "square":
                            {
                                var squareAnn = new SquareAnnotation(page, rect)
                                {
                                    Color = color,
                                    Contents = def.Contents ?? string.Empty
                                };
                                page.Annotations.Add(squareAnn);
                                break;
                            }
                        case "circle":
                            {
                                var circleAnn = new CircleAnnotation(page, rect)
                                {
                                    Color = color,
                                    Contents = def.Contents ?? string.Empty
                                };
                                page.Annotations.Add(circleAnn);
                                break;
                            }
                        case "highlight":
                            {
                                // HighlightAnnotation expects a QuadPoints array; for simplicity we use the rectangle bounds.
                                var highlightAnn = new HighlightAnnotation(page, rect)
                                {
                                    Color = color,
                                    Contents = def.Contents ?? string.Empty
                                };
                                page.Annotations.Add(highlightAnn);
                                break;
                            }
                        default:
                            {
                                Console.Error.WriteLine($"Unsupported annotation type: {def.Type}");
                                break;
                            }
                    }
                }

                // Save the modified document (lifecycle rule – save inside the using block).
                doc.Save(outputPdfPath);
                Console.WriteLine($"Annotated PDF saved to '{outputPdfPath}'.");
            }
        }

        // Helper to convert a simple color name or hex string to Aspose.Pdf.Color.
        private static Aspose.Pdf.Color ResolveColor(string colorStr)
        {
            if (string.IsNullOrWhiteSpace(colorStr))
                return null;

            // Try known named colors.
            switch (colorStr.Trim().ToLowerInvariant())
            {
                case "black":   return Aspose.Pdf.Color.Black;
                case "blue":    return Aspose.Pdf.Color.Blue;
                case "cyan":    return Aspose.Pdf.Color.Cyan;
                case "gray":    return Aspose.Pdf.Color.Gray;
                case "green":   return Aspose.Pdf.Color.Green;
                case "magenta": return Aspose.Pdf.Color.Magenta;
                case "red":     return Aspose.Pdf.Color.Red;
                case "white":   return Aspose.Pdf.Color.White;
                case "yellow":  return Aspose.Pdf.Color.Yellow;
                // Add more named colors as needed.
            }

            // Try hex format "#RRGGBB".
            if (colorStr.StartsWith("#") && colorStr.Length == 7)
            {
                try
                {
                    int r = Convert.ToInt32(colorStr.Substring(1, 2), 16);
                    int g = Convert.ToInt32(colorStr.Substring(3, 2), 16);
                    int b = Convert.ToInt32(colorStr.Substring(5, 2), 16);
                    // Aspose.Pdf.Color.FromRgb expects values in 0‑1 range.
                    return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
                }
                catch
                {
                    // fall through to null
                }
            }

            return null; // unknown format
        }
    }
}