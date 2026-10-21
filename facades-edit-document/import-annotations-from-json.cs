using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationImporter
{
    // Structure that matches the JSON annotation definition
    public class AnnotationDefinition
    {
        public int PageNumber { get; set; }          // 1‑based page index
        public float X { get; set; }                // Lower‑left X coordinate (points)
        public float Y { get; set; }                // Lower‑left Y coordinate (points)
        public float Width { get; set; }            // Width of the annotation rectangle (points)
        public float Height { get; set; }           // Height of the annotation rectangle (points)
        public string? Title { get; set; }          // Optional title of the annotation (nullable)
        public string? Contents { get; set; }       // Text shown when the annotation is opened (nullable)
        public string? Icon { get; set; }           // Icon name (e.g., "Note", "Comment") (nullable)
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string jsonPath = "annotations.json";
            const string outputPdfPath = "annotated_output.pdf";

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

            // ------------------------------------------------------------
            // Load annotation definitions from the JSON file
            // ------------------------------------------------------------
            List<AnnotationDefinition> annotations;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (annotations == null)
                {
                    Console.Error.WriteLine("No annotations found in JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
                return;
            }

            // ------------------------------------------------------------
            // Load the PDF with the high‑level Document API.
            // ------------------------------------------------------------
            Document pdf = new Document(inputPdfPath);

            foreach (var def in annotations)
            {
                // Validate page number (Aspose.Pdf uses 1‑based indexing)
                if (def.PageNumber < 1 || def.PageNumber > pdf.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {def.PageNumber} – skipping annotation.");
                    continue;
                }

                // Build the rectangle (lower‑left X/Y, upper‑right X/Y)
                double llx = def.X;
                double lly = def.Y;
                double urx = def.X + def.Width;
                double ury = def.Y + def.Height;
                Rectangle rect = new Rectangle(llx, lly, urx, ury);

                // Resolve the icon – default to Note if parsing fails or is empty
                TextIcon icon = TextIcon.Note;
                if (!string.IsNullOrWhiteSpace(def.Icon) &&
                    Enum.TryParse<TextIcon>(def.Icon, true, out TextIcon parsedIcon))
                {
                    icon = parsedIcon;
                }

                // Create the TextAnnotation and set optional properties
                Page page = pdf.Pages[def.PageNumber];
                TextAnnotation textAnnot = new TextAnnotation(page, rect)
                {
                    Icon = icon
                };

                if (!string.IsNullOrWhiteSpace(def.Title))
                    textAnnot.Title = def.Title;

                if (!string.IsNullOrWhiteSpace(def.Contents))
                    textAnnot.Contents = def.Contents;

                // Add the annotation to the page
                page.Annotations.Add(textAnnot);
            }

            // Save the annotated PDF
            pdf.Save(outputPdfPath);
            Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
        }
    }
}
