using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AnnotationBatchDeletion
{
    // Configuration model matching the JSON structure
    public class DeletionConfig
    {
        public List<string> RetainTypes { get; set; } = new List<string>();
    }

    class Program
    {
        // Sample JSON configuration (could be stored in a separate file)
        // {
        //   "retainTypes": [ "Text", "Highlight", "Underline" ]
        // }
        static readonly string SampleConfigJson = @"
{
    ""retainTypes"": [ ""Text"", ""Highlight"", ""Underline"" ]
}
";

        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output_cleaned.pdf";
            const string configPath = "deletionConfig.json";

            // Write sample config to file if it does not exist (for demonstration)
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, SampleConfigJson);
            }

            // Load configuration
            DeletionConfig config = LoadConfig(configPath);

            // Ensure the input PDF exists
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Process the PDF: delete all annotation types except those listed in the config
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPdfPath);

                // Aspose.Pdf does not expose an AnnotationType enum. Instead we provide a list of
                // annotation type names that DeleteAnnotations(string) understands.
                string[] allAnnotationTypes = new string[]
                {
                    "Text", "Link", "Highlight", "Underline", "Squiggly", "StrikeOut",
                    "Caret", "Ink", "FileAttachment", "Sound", "Movie", "Screen",
                    "Widget", "Popup", "FreeText", "Line", "Square", "Circle",
                    "Polygon", "PolyLine", "Stamp", "RichMedia"
                };

                // Delete each type that is NOT in the retain list
                foreach (string typeName in allAnnotationTypes)
                {
                    if (config.RetainTypes.Contains(typeName, StringComparer.OrdinalIgnoreCase))
                    {
                        // Skip deletion for retained types
                        continue;
                    }

                    // Delete all annotations of this type
                    editor.DeleteAnnotations(typeName);
                }

                // Save the cleaned PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations cleaned. Output saved to '{outputPdfPath}'.");
        }

        // Helper method to deserialize the JSON configuration file
        private static DeletionConfig LoadConfig(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<DeletionConfig>(json) ?? new DeletionConfig();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load config: {ex.Message}");
                // Return an empty config to avoid accidental deletions
                return new DeletionConfig();
            }
        }
    }
}