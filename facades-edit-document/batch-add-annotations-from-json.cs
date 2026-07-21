using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace BatchAnnotationExample
{
    // Model representing a single annotation definition from the JSON file
    public class AnnotationDefinition
    {
        public string Type { get; set; }          // e.g., "Text", "Highlight"
        public int Page { get; set; }             // 1‑based page number
        public double[] Rect { get; set; }        // [llx, lly, urx, ury]
        public string Contents { get; set; }      // Text or comment
        public string Color { get; set; }         // Optional hex color (e.g., "#FF0000")
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

            // Deserialize JSON into a list of annotation definitions
            List<AnnotationDefinition> annotations;
            try
            {
                string json = File.ReadAllText(jsonDefPath);
                annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            // Process the PDF and add annotations
            try
            {
                using (Document doc = new Document(inputPdfPath))
                {
                    foreach (var def in annotations)
                    {
                        // Validate page number (Aspose.Pdf uses 1‑based indexing)
                        if (def.Page < 1 || def.Page > doc.Pages.Count)
                        {
                            Console.Error.WriteLine($"Invalid page number {def.Page} for annotation; skipping.");
                            continue;
                        }

                        // Validate rectangle coordinates
                        if (def.Rect == null || def.Rect.Length != 4)
                        {
                            Console.Error.WriteLine("Rectangle must contain exactly four numbers; skipping annotation.");
                            continue;
                        }

                        // Create a fully qualified rectangle
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            def.Rect[0], def.Rect[1], def.Rect[2], def.Rect[3]);

                        // Determine color (fallback to Black)
                        Aspose.Pdf.Color color = Aspose.Pdf.Color.Black;
                        if (!string.IsNullOrWhiteSpace(def.Color))
                        {
                            try
                            {
                                // Simple hex parser: #RRGGBB
                                if (def.Color.StartsWith("#") && def.Color.Length == 7)
                                {
                                    int r = Convert.ToInt32(def.Color.Substring(1, 2), 16);
                                    int g = Convert.ToInt32(def.Color.Substring(3, 2), 16);
                                    int b = Convert.ToInt32(def.Color.Substring(5, 2), 16);
                                    // Aspose.Pdf.Color.FromRgb expects values in 0‑1 range
                                    color = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
                                }
                            }
                            catch
                            {
                                // If parsing fails, keep default Black
                            }
                        }

                        // Retrieve the target page
                        Page page = doc.Pages[def.Page];

                        // Add annotation based on the specified type
                        switch (def.Type?.Trim().ToLowerInvariant())
                        {
                            case "text":
                                {
                                    // TextAnnotation constructor: (Page, Rectangle)
                                    TextAnnotation textAnn = new TextAnnotation(page, rect)
                                    {
                                        Title    = "Note",
                                        Contents = def.Contents ?? string.Empty,
                                        Color    = color,
                                        Open     = true
                                    };
                                    page.Annotations.Add(textAnn);
                                    break;
                                }
                            case "highlight":
                                {
                                    // HighlightAnnotation constructor: (Page, Rectangle)
                                    HighlightAnnotation highlightAnn = new HighlightAnnotation(page, rect)
                                    {
                                        Contents = def.Contents ?? string.Empty,
                                        Color    = color
                                    };
                                    page.Annotations.Add(highlightAnn);
                                    break;
                                }
                            case "link":
                                {
                                    // LinkAnnotation with a web URL (example uses Hyperlink property via GoToURIAction)
                                    LinkAnnotation linkAnn = new LinkAnnotation(page, rect);
                                    // Use GoToURIAction to set external URL; if Contents holds the URL
                                    if (!string.IsNullOrWhiteSpace(def.Contents))
                                    {
                                        linkAnn.Action = new GoToURIAction(def.Contents);
                                    }
                                    linkAnn.Color = color;
                                    page.Annotations.Add(linkAnn);
                                    break;
                                }
                            // Additional annotation types can be added here following the same pattern
                            default:
                                {
                                    Console.Error.WriteLine($"Unsupported annotation type '{def.Type}'; skipping.");
                                    break;
                                }
                        }
                    }

                    // Save the modified PDF (PDF format, no SaveOptions needed)
                    doc.Save(outputPdfPath);
                }

                Console.WriteLine($"Annotated PDF saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
        }
    }
}