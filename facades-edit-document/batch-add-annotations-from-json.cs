using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    // Model matching the expected JSON structure
    private class AnnotationDefinition
    {
        public string Type { get; set; }          // e.g., "Text", "Highlight", "Square", "Link"
        public int PageNumber { get; set; }       // 1‑based page index
        public double Llx { get; set; }           // lower‑left X
        public double Lly { get; set; }           // lower‑left Y
        public double Urx { get; set; }           // upper‑right X
        public double Ury { get; set; }           // upper‑right Y
        public string Contents { get; set; }      // annotation text or URL
        public string Color { get; set; }         // optional, e.g., "Red", "Yellow"
    }

    static void Main(string[] args)
    {
        // Expected arguments: inputPdf jsonDefinition outputPdf
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <definitionJson> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string jsonPath = args[1];
        string outputPdfPath = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON definition file not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of definitions
        List<AnnotationDefinition> definitions;
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

        // Process PDF
        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                foreach (var def in definitions)
                {
                    // Validate page number
                    if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {def.PageNumber} for annotation; skipping.");
                        continue;
                    }

                    Page page = doc.Pages[def.PageNumber];

                    // Build rectangle (fully qualified to avoid ambiguity)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(def.Llx, def.Lly, def.Urx, def.Ury);

                    // Resolve optional color
                    Aspose.Pdf.Color aspColor = null;
                    if (!string.IsNullOrWhiteSpace(def.Color))
                    {
                        // Simple mapping; extend as needed
                        switch (def.Color.Trim().ToLowerInvariant())
                        {
                            case "red": aspColor = Aspose.Pdf.Color.Red; break;
                            case "green": aspColor = Aspose.Pdf.Color.Green; break;
                            case "blue": aspColor = Aspose.Pdf.Color.Blue; break;
                            case "yellow": aspColor = Aspose.Pdf.Color.Yellow; break;
                            case "gray": aspColor = Aspose.Pdf.Color.Gray; break;
                            case "lightgray": aspColor = Aspose.Pdf.Color.LightGray; break;
                            default: aspColor = Aspose.Pdf.Color.Black; break;
                        }
                    }

                    // Create appropriate annotation based on type
                    switch (def.Type?.Trim().ToLowerInvariant())
                    {
                        case "text":
                            {
                                TextAnnotation ann = new TextAnnotation(page, rect)
                                {
                                    Title = "Note",
                                    Contents = def.Contents ?? string.Empty,
                                    Open = true,
                                    Color = aspColor ?? Aspose.Pdf.Color.Yellow
                                };
                                page.Annotations.Add(ann);
                                break;
                            }
                        case "highlight":
                            {
                                HighlightAnnotation ann = new HighlightAnnotation(page, rect)
                                {
                                    Color = aspColor ?? Aspose.Pdf.Color.Yellow
                                };
                                // Highlight annotations use the Contents property for the highlighted text (optional)
                                if (!string.IsNullOrEmpty(def.Contents))
                                    ann.Contents = def.Contents;
                                page.Annotations.Add(ann);
                                break;
                            }
                        case "square":
                            {
                                SquareAnnotation ann = new SquareAnnotation(page, rect)
                                {
                                    Color = aspColor ?? Aspose.Pdf.Color.LightGray,
                                    Contents = def.Contents ?? string.Empty
                                };
                                page.Annotations.Add(ann);
                                break;
                            }
                        case "circle":
                            {
                                CircleAnnotation ann = new CircleAnnotation(page, rect)
                                {
                                    Color = aspColor ?? Aspose.Pdf.Color.LightGray,
                                    Contents = def.Contents ?? string.Empty
                                };
                                page.Annotations.Add(ann);
                                break;
                            }
                        case "link":
                            {
                                LinkAnnotation ann = new LinkAnnotation(page, rect);
                                // If the Contents field looks like a URL, create a GoToURIAction
                                if (!string.IsNullOrWhiteSpace(def.Contents))
                                {
                                    ann.Action = new GoToURIAction(def.Contents);
                                    ann.Color = aspColor ?? Aspose.Pdf.Color.Blue;
                                }
                                page.Annotations.Add(ann);
                                break;
                            }
                        default:
                            {
                                Console.Error.WriteLine($"Unsupported annotation type '{def.Type}'; skipping.");
                                break;
                            }
                    }
                }

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPdfPath);
                Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}