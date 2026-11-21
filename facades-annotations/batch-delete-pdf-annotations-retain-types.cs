using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationRetentionConfig
{
    public List<string> RetainAnnotationTypes { get; set; }

    public AnnotationRetentionConfig()
    {
        // Ensure the list is never null
        RetainAnnotationTypes = new List<string>();
    }
}

class Program
{
    static void Main()
    {
        const string configPath = "config.json";
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Create a sample configuration file if it does not exist
        if (!File.Exists(configPath))
        {
            var sampleConfig = new AnnotationRetentionConfig
            {
                RetainAnnotationTypes = new List<string> { "Text", "Highlight", "Stamp" }
            };
            string json = JsonSerializer.Serialize(sampleConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Sample configuration written to {configPath}");
        }

        // Load configuration
        AnnotationRetentionConfig config;
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<AnnotationRetentionConfig>(jsonContent);
            if (config == null || config.RetainAnnotationTypes == null)
                throw new InvalidOperationException("Configuration is invalid.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load configuration: {ex.Message}");
            return;
        }

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Open PDF and delete unwanted annotations
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                AnnotationCollection annots = page.Annotations;
                // Iterate backwards because we will delete items
                for (int i = annots.Count - 1; i >= 0; i--)
                {
                    Annotation annot = annots[i];
                    // Use AnnotationType property (not Subtype) to get the annotation kind
                    string typeName = annot.AnnotationType.ToString(); // e.g., "Text", "Highlight", "Stamp"
                    if (!config.RetainAnnotationTypes.Contains(typeName))
                    {
                        annots.Delete(i);
                    }
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations filtered. Result saved to {outputPdf}");
    }
}
