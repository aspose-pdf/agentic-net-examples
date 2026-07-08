using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // PdfAnnotationEditor
using Aspose.Pdf.Annotations;           // AnnotationType, TextAnnotation, HighlightAnnotation
using Aspose.Pdf.Text;                  // TextFragment

namespace BatchAnnotationDeletion
{
    // Model for the JSON configuration
    public class DeletionConfig
    {
        // List of annotation type names (e.g., "Text", "Highlight") to keep
        public List<string> RetainTypes { get; set; } = new List<string>();
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath   = "input.pdf";
            const string outputPdfPath  = "output.pdf";
            const string configJsonPath = "deletionConfig.json";

            // Ensure a source PDF exists – create a minimal one if it does not.
            EnsureSamplePdfExists(inputPdfPath);

            // Load configuration JSON (creates a default file if missing)
            DeletionConfig config = LoadConfig(configJsonPath);

            // Normalize retained type names for case‑insensitive comparison
            var retainSet = new HashSet<string>(config.RetainTypes, StringComparer.OrdinalIgnoreCase);

            // Use PdfAnnotationEditor (facade) to edit annotations
            using (var editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPdfPath);

                // Iterate over all possible AnnotationType enum values
                foreach (AnnotationType annotType in Enum.GetValues(typeof(AnnotationType)))
                {
                    string typeName = annotType.ToString();

                    // Delete the annotation type only if it is NOT in the retain list
                    if (!retainSet.Contains(typeName))
                    {
                        // DeleteAnnotations(string) removes all annotations of the given type
                        editor.DeleteAnnotations(typeName);
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations processed. Output saved to '{outputPdfPath}'.");
        }

        // Creates a very simple PDF with a couple of annotations if the file does not exist.
        private static void EnsureSamplePdfExists(string path)
        {
            if (File.Exists(path))
                return;

            var doc = new Document();
            var page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for annotation deletion demo."));

            // Add a Text annotation (type "Text")
            var textAnn = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = "Sample Note",
                Contents = "This annotation will be retained if 'Text' is in the config."
            };
            page.Annotations.Add(textAnn);

            // Add a Highlight annotation (type "Highlight")
            var highlight = new HighlightAnnotation(page, new Rectangle(100, 500, 300, 520))
            {
                Color = Color.Yellow,
                Contents = "Highlight annotation."
            };
            page.Annotations.Add(highlight);

            doc.Save(path);
            Console.WriteLine($"Sample PDF created at '{path}'.");
        }

        // Helper method to read and deserialize the JSON configuration file.
        // If the file does not exist, a default configuration file is created and returned.
        private static DeletionConfig LoadConfig(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                // Create a sample configuration with a few common annotation types to retain.
                var sampleConfig = new DeletionConfig
                {
                    RetainTypes = new List<string> { "Text", "Highlight", "Underline" }
                };

                string sampleJson = JsonSerializer.Serialize(sampleConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, sampleJson);
                Console.WriteLine($"Configuration file not found. A default file has been created at '{jsonPath}'.");
                return sampleConfig;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            DeletionConfig? config = JsonSerializer.Deserialize<DeletionConfig>(jsonContent);
            if (config == null)
                throw new InvalidOperationException("Failed to deserialize configuration.");

            return config;
        }
    }
}
