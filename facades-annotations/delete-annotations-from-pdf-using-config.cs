using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;                     // Document, Page, AnnotationType
using Aspose.Pdf.Annotations;        // AnnotationType enum

class Program
{
    // Simple POCO matching the JSON configuration file
    private class AnnotationConfig
    {
        // Initialise with an empty array to satisfy the non‑nullable warning
        public string[] AnnotationTypes { get; set; } = Array.Empty<string>();
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "annotationConfig.json";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Create a default configuration file if it does not exist
        // -----------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            AnnotationConfig defaultConfig = new AnnotationConfig {
                AnnotationTypes = new[] { "Text", "Highlight", "Underline" }
            };
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at '{configPath}'.");
        }

        // -----------------------------------------------------------------
        // Step 2: Read the configuration file and parse annotation types
        // -----------------------------------------------------------------
        AnnotationConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<AnnotationConfig>(json)!; // non‑null after validation
            if (config?.AnnotationTypes == null || config.AnnotationTypes.Length == 0)
            {
                Console.Error.WriteLine("Configuration file does not specify any annotation types.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse config file: {ex.Message}");
            return;
        }

        // Convert string names to the Aspose.Pdf.Annotations.AnnotationType enum
        List<AnnotationType> typesList = new List<AnnotationType>();
        foreach (string typeName in config.AnnotationTypes)
        {
            try
            {
                // Enum.Parse is case‑insensitive (ignoreCase:true)
                AnnotationType type = (AnnotationType)Enum.Parse(typeof(AnnotationType), typeName, true);
                typesList.Add(type);
            }
            catch (ArgumentException)
            {
                Console.Error.WriteLine($"Unknown annotation type in config: '{typeName}'. Skipping.");
            }
        }

        if (typesList.Count == 0)
        {
            Console.Error.WriteLine("No valid annotation types were found in the configuration.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 3: Delete the specified annotations using Document API
        // -----------------------------------------------------------------
        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Iterate over each page and remove matching annotations
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate backwards because we will be deleting items
                for (int i = page.Annotations.Count - 1; i >= 0; i--)
                {
                    var annotation = page.Annotations[i];
                    if (typesList.Contains(annotation.AnnotationType))
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations deleted. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation deletion: {ex.Message}");
        }
    }
}
