using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Config
{
    // List of annotation type names to keep (e.g., "Text", "Highlight")
    public List<string> RetainAnnotationTypes { get; set; }
}

class Program
{
    static void Main()
    {
        // Path to the JSON configuration file
        const string configPath = "config.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize configuration
        Config config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));
        if (config?.RetainAnnotationTypes == null)
        {
            Console.Error.WriteLine("Invalid configuration: RetainAnnotationTypes missing.");
            return;
        }

        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Initialize the annotation editor and bind the source PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document to work with pages and annotations
        Document doc = editor.Document;

        // Iterate through all pages (1‑based indexing)
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            Page page = doc.Pages[pageIndex];
            AnnotationCollection annotations = page.Annotations;

            // Iterate backwards because deleting shifts indices
            for (int annIndex = annotations.Count; annIndex >= 1; annIndex--)
            {
                Annotation annotation = annotations[annIndex];

                // Convert the enum value to its name (e.g., "Text", "Highlight")
                string typeName = annotation.AnnotationType.ToString();

                // Delete the annotation if its type is NOT in the retain list
                if (!config.RetainAnnotationTypes.Contains(typeName, StringComparer.OrdinalIgnoreCase))
                {
                    annotations.Delete(annIndex);
                }
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}