using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfAnnotationDeletion
{
    // Simple POCO to deserialize the configuration file.
    public class DeletionConfig
    {
        public List<string> AnnotationTypes { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Paths – adjust as needed.
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output.pdf";
            const string configPath = "deletionConfig.json";

            // Validate input files.
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load the configuration file.
            DeletionConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<DeletionConfig>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Initialize the PdfAnnotationEditor facade.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the source PDF.
            editor.BindPdf(inputPdfPath);

            // If no specific types are provided, delete all annotations.
            if (config?.AnnotationTypes == null || config.AnnotationTypes.Count == 0)
            {
                editor.DeleteAnnotations(); // Deletes every annotation in the document.
            }
            else
            {
                // Delete annotations for each specified type.
                foreach (string annotType in config.AnnotationTypes)
                {
                    // Guard against empty or whitespace entries.
                    if (string.IsNullOrWhiteSpace(annotType))
                        continue;

                    editor.DeleteAnnotations(annotType);
                }
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);

            Console.WriteLine($"Annotations deleted as per configuration. Output saved to '{outputPdfPath}'.");
        }
    }
}