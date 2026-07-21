using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AnnotationBatchDeletion
{
    // Represents the JSON configuration file.
    public class AnnotationRetentionConfig
    {
        // List of annotation type names to keep (case‑insensitive).
        public List<string> RetainTypes { get; set; } = new List<string>();
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output.pdf";
            const string configPath = "config.json";

            // Ensure an input PDF exists for the demo (self‑contained example).
            if (!File.Exists(inputPdfPath))
            {
                using (Document seed = new Document())
                {
                    seed.Pages.Add(); // add a blank page
                    seed.Save(inputPdfPath);
                }
            }

            // Load retention configuration.
            AnnotationRetentionConfig config = LoadConfig(configPath);

            // All known annotation type names (as accepted by DeleteAnnotations(string)).
            // NOTE: "Redact" is not a valid type name for Aspose.Pdf's DeleteAnnotations method and
            // therefore has been removed to avoid the runtime ArgumentException.
            string[] allAnnotationTypes = new[]
            {
                "Text", "Highlight", "Underline", "Squiggly", "StrikeOut",
                "Line", "Square", "Circle", "Polygon", "PolyLine", "Ink",
                "Stamp", "FileAttachment", "Sound", "Movie", "Link",
                "Popup", "FreeText", "Caret"
            };

            // Determine which types must be deleted.
            var typesToDelete = allAnnotationTypes
                .Except(config.RetainTypes ?? Enumerable.Empty<string>(),
                         StringComparer.OrdinalIgnoreCase)
                .ToList();

            // If nothing to delete, just copy the file.
            if (!typesToDelete.Any())
            {
                File.Copy(inputPdfPath, outputPdfPath, overwrite: true);
                Console.WriteLine("No annotations were removed – output copied unchanged.");
                return;
            }

            // Use PdfAnnotationEditor to delete unwanted annotation types.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF.
                editor.BindPdf(inputPdfPath);

                // Delete each unwanted type.
                foreach (string annotType in typesToDelete)
                {
                    editor.DeleteAnnotations(annotType);
                }

                // Save the cleaned PDF.
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations retained: {string.Join(", ", config.RetainTypes ?? Enumerable.Empty<string>())}");
            Console.WriteLine($"Deleted annotation types: {string.Join(", ", typesToDelete)}");
            Console.WriteLine($"Result saved to '{outputPdfPath}'.");
        }

        // Reads the JSON configuration file and deserializes it.
        private static AnnotationRetentionConfig LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                // If the config file is missing, default to retaining no annotations.
                return new AnnotationRetentionConfig();
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AnnotationRetentionConfig>(json)
                   ?? new AnnotationRetentionConfig();
        }
    }
}
