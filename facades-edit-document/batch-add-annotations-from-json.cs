using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfBatchAnnotation
{
    // Represents a single annotation instruction read from JSON
    public class AnnotationDefinition
    {
        public string? InputPdf { get; set; }          // Path to source PDF
        public string? OutputPdf { get; set; }         // Path where annotated PDF will be saved
        public int PageNumber { get; set; }           // 1‑based page index
        public string? Type { get; set; }              // "Text", "Link", "Stamp", "Highlight"
        public double[]? Rect { get; set; }            // [llx, lly, urx, ury] in points
        public string? Content { get; set; }           // Text for Text/Highlight annotations
        public string? Url { get; set; }               // Target URL for Link annotations
        public string? ImagePath { get; set; }         // Image file for Stamp annotations
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath = "annotations.json";

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"Definition file not found: {jsonPath}");
                return;
            }

            // Deserialize JSON into a list of annotation definitions
            List<AnnotationDefinition>? definitions;
            try
            {
                string json = File.ReadAllText(jsonPath);
                definitions = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (definitions == null || definitions.Count == 0)
            {
                Console.WriteLine("No annotation definitions found.");
                return;
            }

            // Process each definition independently
            foreach (var def in definitions)
            {
                if (string.IsNullOrWhiteSpace(def.InputPdf) || !File.Exists(def.InputPdf))
                {
                    Console.Error.WriteLine($"Input PDF not found: {def.InputPdf}");
                    continue;
                }

                // Ensure rectangle coordinates are valid
                if (def.Rect == null || def.Rect.Length != 4)
                {
                    Console.Error.WriteLine($"Invalid rectangle for {def.InputPdf} page {def.PageNumber}");
                    continue;
                }

                // Wrap Document in a using block for deterministic disposal
                using (Document doc = new Document(def.InputPdf))
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing)
                    if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page number {def.PageNumber} out of range for {def.InputPdf}");
                        continue;
                    }

                    // Build the rectangle using Aspose.Pdf.Rectangle
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        def.Rect[0], def.Rect[1], def.Rect[2], def.Rect[3]);

                    // Add the appropriate annotation based on the Type field
                    switch (def.Type?.Trim().ToLowerInvariant())
                    {
                        case "text":
                            {
                                // TextAnnotation displays a pop‑up note
                                TextAnnotation txtAnn = new TextAnnotation(doc.Pages[def.PageNumber], rect)
                                {
                                    Title = "Note",
                                    Contents = def.Content ?? string.Empty,
                                    Open = true,
                                    Icon = TextIcon.Note
                                };
                                doc.Pages[def.PageNumber].Annotations.Add(txtAnn);
                                break;
                            }
                        case "link":
                            {
                                // LinkAnnotation creates a clickable hyperlink
                                LinkAnnotation linkAnn = new LinkAnnotation(doc.Pages[def.PageNumber], rect)
                                {
                                    Action = new GoToURIAction(def.Url ?? string.Empty)
                                };
                                doc.Pages[def.PageNumber].Annotations.Add(linkAnn);
                                break;
                            }
                        case "stamp":
                            {
                                // StampAnnotation places an image on the page
                                if (string.IsNullOrEmpty(def.ImagePath) || !File.Exists(def.ImagePath))
                                {
                                    Console.Error.WriteLine($"Stamp image not found: {def.ImagePath}");
                                    break;
                                }

                                ImageStamp stamp = new ImageStamp(def.ImagePath);
                                // Set size based on rectangle
                                double width = rect.URX - rect.LLX;
                                double height = rect.URY - rect.LLY;
                                stamp.Width = width;
                                stamp.Height = height;
                                // Position using alignment + indents (Left/Bottom alignment)
                                stamp.HorizontalAlignment = HorizontalAlignment.Left;
                                stamp.VerticalAlignment = VerticalAlignment.Bottom;
                                stamp.XIndent = rect.LLX;
                                stamp.YIndent = rect.LLY;

                                // Apply the stamp to the target page
                                doc.Pages[def.PageNumber].AddStamp(stamp);
                                break;
                            }
                        case "highlight":
                            {
                                // HighlightAnnotation highlights a region of text
                                HighlightAnnotation hlAnn = new HighlightAnnotation(doc.Pages[def.PageNumber], rect)
                                {
                                    Color = Color.FromRgb(1.0, 1.0, 0.0), // Yellow
                                    Opacity = 0.5,
                                    Contents = def.Content ?? string.Empty
                                };
                                doc.Pages[def.PageNumber].Annotations.Add(hlAnn);
                                break;
                            }
                        default:
                            {
                                Console.Error.WriteLine($"Unsupported annotation type: {def.Type}");
                                break;
                            }
                    }

                    // Save the modified PDF; the output path may be the same as input or different
                    string outputPath = def.OutputPdf ?? def.InputPdf;
                    doc.Save(outputPath);
                    Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
                }
            }
        }
    }
}
