using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Configuration model matching the JSON structure
    private class Config
    {
        // Initialise the collection to satisfy non‑nullable analysis
        public List<string> RetainAnnotationTypes { get; set; } = new();
    }

    static void Main()
    {
        // Paths for the source PDF, the output PDF and the JSON config file
        const string inputPdfPath = "source.pdf";
        const string outputPdfPath = "cleaned.pdf";
        const string configPath = "annotationConfig.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load the JSON configuration that lists annotation types to keep
        // Example JSON:
        // {
        //   "RetainAnnotationTypes": [ "Highlight", "Underline", "Text" ]
        // }
        string json = File.ReadAllText(configPath);
        Config cfg = JsonSerializer.Deserialize<Config>(json);

        if (cfg == null || cfg.RetainAnnotationTypes == null)
        {
            Console.Error.WriteLine("Invalid configuration.");
            return;
        }

        // Normalise the retain list for case‑insensitive comparison
        var retainSet = new HashSet<string>(cfg.RetainAnnotationTypes, StringComparer.OrdinalIgnoreCase);

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Iterate through each page and remove annotations that are NOT in the retain list
            foreach (Page page in pdfDoc.Pages)
            {
                // Collect annotations to delete (cannot modify collection while iterating)
                var toDelete = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    // ann.AnnotationType is an enum; compare its name with the retain list
                    if (!retainSet.Contains(ann.AnnotationType.ToString()))
                    {
                        toDelete.Add(ann);
                    }
                }

                // Delete the collected annotations
                foreach (var ann in toDelete)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the cleaned PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations cleaned. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation processing: {ex.Message}");
        }
    }
}
