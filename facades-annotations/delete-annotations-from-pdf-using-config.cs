using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

// Simple POCO to match the JSON configuration file
public class AnnotationConfig
{
    public List<string> DeleteTypes { get; set; } = new List<string>();
}

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputPdf = "sample_cleaned.pdf";
        const string configPath = "annotConfig.json";

        // -----------------------------------------------------------------
        // 1. Create a sample configuration file (if it does not already exist)
        // -----------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            AnnotationConfig sampleConfig = new AnnotationConfig {
                DeleteTypes = new List<string> { "Text", "Highlight", "Line" }
            };
            string json = JsonSerializer.Serialize(sampleConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created sample config at '{configPath}'.");
        }

        // -----------------------------------------------------------------
        // 2. Load the configuration file
        // -----------------------------------------------------------------
        AnnotationConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<AnnotationConfig>(json) ?? new AnnotationConfig();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 3. Delete the specified annotation types using PdfAnnotationEditor
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so we use a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF file.
                editor.BindPdf(inputPdf);

                // Iterate over the annotation types defined in the config file.
                foreach (string annotType in config.DeleteTypes)
                {
                    // Delete all annotations of the current type.
                    // The method expects the annotation type name as a string (e.g., "Text", "Highlight").
                    editor.DeleteAnnotations(annotType);
                }

                // Save the modified PDF.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Annotations removed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}