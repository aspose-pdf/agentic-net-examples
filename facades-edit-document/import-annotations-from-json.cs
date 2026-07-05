using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAnnotationImport
{
    // Represents a single annotation definition read from JSON.
    public class AnnotationDefinition
    {
        public string? Type { get; set; }               // e.g., "Text"
        public float[]? Rect { get; set; }              // [llx, lly, urx, ury]
        public string? Contents { get; set; }           // Annotation text/content
        public string? Author { get; set; }             // Author name (optional)
        public bool Open { get; set; } = false;        // Whether the annotation is open
        public string? Subject { get; set; }            // Subject (optional)
        public int Icon { get; set; } = 0;              // Icon index for text annotation
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

            // Read and deserialize the JSON file containing annotation definitions.
            List<AnnotationDefinition> annotations;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(jsonContent) ?? new List<AnnotationDefinition>();
                if (annotations.Count == 0)
                {
                    Console.Error.WriteLine("No annotations found in JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            // Use PdfContentEditor to bind the PDF and add annotations.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the existing PDF file.
                editor.BindPdf(inputPdfPath);

                foreach (var def in annotations)
                {
                    // Validate rectangle data.
                    if (def.Rect == null || def.Rect.Length != 4)
                    {
                        Console.Error.WriteLine("Invalid rectangle definition; skipping annotation.");
                        continue;
                    }

                    // Convert the float array to a System.Drawing.Rectangle.
                    // System.Drawing.Rectangle expects (x, y, width, height) where x/y are the lower‑left corner.
                    // Aspose's rectangle is defined by llx,lly,urx,ury, so width = urx-llx, height = ury-lly.
                    var drawingRect = new System.Drawing.Rectangle(
                        (int)def.Rect[0],                     // X (llx)
                        (int)def.Rect[1],                     // Y (lly)
                        (int)(def.Rect[2] - def.Rect[0]),     // Width
                        (int)(def.Rect[3] - def.Rect[1])      // Height
                    );

                    // Determine annotation type and invoke the appropriate creation method.
                    // Currently only "Text" annotations are handled; extend as needed.
                    switch (def.Type?.Trim().ToLowerInvariant())
                    {
                        case "text":
                            // Create a text annotation using the System.Drawing.Rectangle overload.
                            editor.CreateText(
                                drawingRect,
                                def.Contents ?? string.Empty,
                                def.Author ?? "Author",
                                def.Open,
                                def.Subject ?? string.Empty,
                                def.Icon
                            );
                            break;

                        // Add handling for other annotation types here, e.g.:
                        // case "highlight":
                        //     editor.CreateHighlight(drawingRect, ...);
                        //     break;

                        default:
                            Console.Error.WriteLine($"Unsupported annotation type '{def.Type}'; skipping.");
                            break;
                    }
                }

                // Save the modified PDF.
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
        }
    }
}
