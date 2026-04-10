using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

namespace BatchAnnotationExample
{
    // Represents a single annotation definition from the JSON file
    public class AnnotationDefinition
    {
        public string? Type { get; set; }          // e.g., "Text", "Link", "Highlight"
        public int Page { get; set; }              // 1‑based page number
        public float[]? Rect { get; set; }         // [llx, lly, urx, ury]
        public string? Content { get; set; }       // Text for Text/Highlight annotation
        public string? Author { get; set; }        // Optional author name (used as Subject for Text)
        public string? Title { get; set; }         // Optional title for Text annotation
        public string? Url { get; set; }           // URL for Link annotation
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "annotated_output.pdf";
            const string jsonDefinitionPath = "annotations.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(jsonDefinitionPath))
            {
                Console.Error.WriteLine($"JSON definition file not found: {jsonDefinitionPath}");
                return;
            }

            // Deserialize JSON array of annotation definitions
            List<AnnotationDefinition> annotations;
            try
            {
                string json = File.ReadAllText(jsonDefinitionPath);
                annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<AnnotationDefinition>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read JSON definitions: {ex.Message}");
                return;
            }

            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            foreach (var def in annotations)
            {
                // Validate rectangle data
                if (def.Rect == null || def.Rect.Length != 4)
                {
                    Console.Error.WriteLine($"Invalid rectangle for annotation on page {def.Page}");
                    continue;
                }

                // Create a fully qualified rectangle (Aspose.Pdf.Rectangle expects llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    def.Rect[0], def.Rect[1], def.Rect[2], def.Rect[3]);

                // Ensure the page number is within range
                if (def.Page < 1 || def.Page > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {def.Page} is out of range.");
                    continue;
                }

                Page page = pdfDoc.Pages[def.Page];

                switch (def.Type?.Trim().ToLowerInvariant())
                {
                    case "text":
                        // Text (sticky‑note) annotation
                        var textAnnot = new TextAnnotation(page, rect)
                        {
                            Contents = def.Content ?? string.Empty,
                            Title = def.Title ?? "Note",
                            Subject = def.Author ?? "Author"
                            // The "IsOpen" property does not exist in recent versions; omit it or use Open = true if needed.
                        };
                        page.Annotations.Add(textAnnot);
                        break;

                    case "link":
                        // Web link annotation
                        var linkAnnot = new LinkAnnotation(page, rect)
                        {
                            Action = new GoToURIAction(def.Url ?? string.Empty)
                        };
                        page.Annotations.Add(linkAnnot);
                        break;

                    case "highlight":
                        // Highlight markup annotation
                        var highlightAnnot = new HighlightAnnotation(page, rect)
                        {
                            Contents = def.Content ?? string.Empty,
                            Color = Aspose.Pdf.Color.Yellow
                        };
                        page.Annotations.Add(highlightAnnot);
                        break;

                    // Additional annotation types can be added here following the same pattern
                    default:
                        Console.Error.WriteLine($"Unsupported annotation type: {def.Type}");
                        break;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
        }
    }
}
