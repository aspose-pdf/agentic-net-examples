using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationImport
{
    // Model representing a rectangle in the JSON definition
    public class AnnotationRect
    {
        public double LLX { get; set; }
        public double LLY { get; set; }
        public double URX { get; set; }
        public double URY { get; set; }
    }

    // Model representing a single annotation definition
    public class AnnotationDefinition
    {
        public string Type { get; set; }          // e.g., "Text"
        public int Page { get; set; }             // 1‑based page number
        public AnnotationRect Rect { get; set; }  // rectangle coordinates
        public string Contents { get; set; }      // annotation text
        public string Author { get; set; }        // title / author
        public string Subject { get; set; }       // subject
        public string Color { get; set; }         // optional hex color, e.g. "#FF0000"
        public bool Open { get; set; } = true;    // whether the annotation is open
    }

    class Program
    {
        // Convert a hex color string (e.g. "#RRGGBB") to Aspose.Pdf.Color
        private static Aspose.Pdf.Color ParseColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Aspose.Pdf.Color.Black; // default

            // Remove leading '#'
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length != 6)
                return Aspose.Pdf.Color.Black;

            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            // Aspose.Pdf.Color expects values in 0‑1 range
            return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
        }

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
                Console.Error.WriteLine($"JSON definition file not found: {jsonPath}");
                return;
            }

            // Deserialize JSON into a list of annotation definitions
            List<AnnotationDefinition> definitions;
            try
            {
                string json = File.ReadAllText(jsonPath);
                definitions = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
                return;
            }

            // Use PdfContentEditor to bind, modify, and save the document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the existing PDF
                editor.BindPdf(inputPdfPath);

                // Iterate over each definition and create the appropriate annotation
                foreach (var def in definitions)
                {
                    // Validate page number
                    if (def.Page < 1 || def.Page > editor.Document.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {def.Page} for annotation; skipping.");
                        continue;
                    }

                    // Build the rectangle (Aspose.Pdf.Rectangle expects llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        def.Rect.LLX,
                        def.Rect.LLY,
                        def.Rect.URX,
                        def.Rect.URY);

                    // Currently only "Text" annotation type is handled.
                    // Additional types can be added with corresponding Aspose.Pdf.Annotations classes.
                    if (string.Equals(def.Type, "Text", StringComparison.OrdinalIgnoreCase))
                    {
                        // Retrieve the target page (1‑based indexing)
                        Page page = editor.Document.Pages[def.Page];

                        // Create a TextAnnotation and set its properties
                        TextAnnotation textAnnot = new TextAnnotation(page, rect)
                        {
                            Contents = def.Contents ?? string.Empty,
                            Title = def.Author ?? string.Empty,
                            Subject = def.Subject ?? string.Empty,
                            Open = def.Open
                        };

                        // Apply color if provided
                        if (!string.IsNullOrWhiteSpace(def.Color))
                        {
                            textAnnot.Color = ParseColor(def.Color);
                        }

                        // Add the annotation to the page
                        page.Annotations.Add(textAnnot);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unsupported annotation type '{def.Type}'; skipping.");
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
        }
    }
}