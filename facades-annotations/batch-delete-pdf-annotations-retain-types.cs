using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // AnnotationType enum

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF and JSON configuration
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string configPath = "config.json";

        // Validate file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // ----- Load JSON configuration -----
        // Expected format: { "retainTypes": [ "Text", "Highlight" ] }
        string json = File.ReadAllText(configPath);
        Config cfg = JsonSerializer.Deserialize<Config>(json);

        // Build a case‑insensitive set of annotation type names to retain
        HashSet<string> retain = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (cfg?.RetainTypes != null)
        {
            foreach (var t in cfg.RetainTypes) retain.Add(t);
        }

        // ----- Process PDF with PdfAnnotationEditor -----
        // PdfAnnotationEditor follows the create‑load‑save lifecycle:
        //   1. Create new instance
        //   2. BindPdf (load)
        //   3. Save (persist)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Iterate over all possible annotation types defined in the enum
            foreach (AnnotationType type in Enum.GetValues(typeof(AnnotationType)))
            {
                string typeName = type.ToString();

                // Delete the annotation type only if it is NOT in the retain list
                if (!retain.Contains(typeName))
                {
                    // DeleteAnnotations(string) removes all annotations of the given type
                    editor.DeleteAnnotations(typeName);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF processed. Output saved to '{outputPath}'.");
        Console.WriteLine($"Retained annotation types: {string.Join(", ", retain)}");
    }

    // POCO class matching the JSON structure
    private class Config
    {
        public string[] RetainTypes { get; set; }
    }
}